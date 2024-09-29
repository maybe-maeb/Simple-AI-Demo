using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateWork : ControllerState
{
    public StateWork(StateController controller) : base(controller) { }

    private float timeStartedWorking;
    private bool isWorking;
    private bool doneWorking;

    public override void OnStateEnter(){
        Debug.Log("Entered work state.");
        controller.GetComponent<NavMeshAgent>().isStopped = false;
        controller.currentDestination = controller.workSpot.transform;
        doneWorking = false;
        isWorking = false;
    }

    public override void CheckTransitions(){
        //If it finishes working
        if (doneWorking) 
        {
            int rand = Random.Range(0, 100);
            if (rand > 50) controller.SetState(new StatePatrol(controller));
            else controller.SetState(new StateAnger(controller));
        }

        //If the computer breaks, go to the fix object
        if(controller.computer.isBroken) {
            controller.objectToFix = controller.computer.transform.gameObject;
            controller.SetState(new StateFixObject(controller));
        }
    }

    public override void Act(){
        controller.agent.destination = controller.currentDestination.position;
            float dist = Vector3.Distance(controller.transform.position, controller.currentDestination.position);
            if (dist < 3f){
                if (!isWorking){
                    controller.triedToWork = true;
                    timeStartedWorking = Time.time;
                    Debug.Log("Started working at " + timeStartedWorking);
                    isWorking = true;
                }
                else{
                    Debug.Log("Is working...");
                    controller.agent.isStopped = true;
                    if (Time.time - timeStartedWorking >= controller.timeToWork){
                        Debug.Log("Finished working.");
                        controller.triedToWork = false;
                        controller.agent.isStopped = false;
                        doneWorking = true;
                        controller.currentDestination = controller.RandomDestination();
                        controller.agent.destination = controller.currentDestination.position;
                    }
                }
        }
    }

    public override void OnStateExit(){
        controller.agent.isStopped = false;
    }
}
