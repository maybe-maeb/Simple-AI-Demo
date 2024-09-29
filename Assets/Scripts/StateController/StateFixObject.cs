using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateFixObject : ControllerState
{
    public StateFixObject(StateController controller) : base(controller) { }

    private float timeStartedFixing;
    private bool isFixing;
    private bool doneFixing;
    private bool goToAnger;

    public override void OnStateEnter(){
        Debug.Log("Entered fixing state.");
        if (controller.objectToFix != null){
            controller.GetComponent<NavMeshAgent>().isStopped = false;
            controller.currentDestination = controller.objectToFix.transform;
            doneFixing = false;
            isFixing = false;
        }
    }

    public override void CheckTransitions(){
        if (doneFixing || controller.objectToFix == null) {
            if (goToAnger) controller.SetState(new StateAnger(controller));
            else if (controller.triedToWork) controller.SetState(new StateWork(controller));
            else controller.SetState(new StatePatrol(controller));
        }
    }

    public override void Act(){
        if (controller.lastFixTime >= controller.timeBetweenFixToAnger) goToAnger = true;
        controller.agent.destination = controller.currentDestination.position;
        if (!doneFixing){
            float dist = Vector3.Distance(controller.transform.position, controller.currentDestination.position);
            if (dist < 5f){
                if (!isFixing){
                    timeStartedFixing = Time.time;
                    Debug.Log("Started fixing at " + timeStartedFixing);
                    isFixing = true;
                }
                else{
                    controller.agent.isStopped = true;
                    if (Time.time - timeStartedFixing >= controller.timeToFix){
                        Debug.Log("Finished fixing object.");
                        controller.objectToFix.GetComponent<Breakable>().isBroken = false;
                        controller.objectToFix = null;
                        controller.lastFixTime = Time.time;
                        controller.agent.isStopped = false;
                        doneFixing = true;
                    }
                }
            }
            else Debug.Log("Walking to broken object...");
        }
    }

    public override void OnStateExit(){
        controller.agent.isStopped = false;
    }
}
