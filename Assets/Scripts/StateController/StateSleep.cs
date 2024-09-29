using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateSleep : ControllerState
{
    public StateSleep(StateController controller) : base(controller) { }

    private float timeStartedSleeping;
    private bool isSleeping;
    private bool doneSleeping;

    public override void OnStateEnter(){
        Debug.Log("Entered sleep state.");
        controller.GetComponent<NavMeshAgent>().isStopped = false;
        controller.currentDestination = controller.sleepSpot.transform;
        doneSleeping = false;
        isSleeping = false;
    }

    public override void CheckTransitions(){
        //If it finishes working
        if (doneSleeping) controller.SetState(new StatePatrol(controller));
    }

    public override void Act(){
        controller.agent.destination = controller.currentDestination.position;
            float dist = Vector3.Distance(controller.transform.position, controller.currentDestination.position);
            if (dist < 3f){
                if (!isSleeping){
                    timeStartedSleeping = Time.time;
                    Debug.Log("Started sleeping at " + timeStartedSleeping);
                    isSleeping = true;
                }
                else{
                    Debug.Log("Is sleeping...");
                    controller.agent.isStopped = true;
                    if (Time.time - timeStartedSleeping >= controller.timeToSleep){
                        Debug.Log("Finished sleeping.");
                        controller.agent.isStopped = false;
                        doneSleeping = true;
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
