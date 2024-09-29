using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatrol : ControllerState
{
    public StatePatrol(StateController controller) : base(controller) { }

    private float timeStartedPatrol;

    public override void OnStateEnter(){
        Debug.Log("Entered patrol state.");
        controller.GetComponent<NavMeshAgent>().isStopped = false;
        controller.currentDestination = controller.RandomDestination();
        timeStartedPatrol = Time.time;
    }

    public override void CheckTransitions(){
        List<GameObject> breakables = new List<GameObject>();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Breakable")){
            breakables.Add(go);
        }

        foreach (GameObject go in breakables){
            controller.scanner.transform.LookAt(go.transform.position);
            RaycastHit hit;
	        if(Physics.Raycast(controller.scanner.transform.position, controller.scanner.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)){
                if (hit.transform.GetComponent<Breakable>() != null && hit.transform.GetComponent<Breakable>().isBroken == true){
                    controller.objectToFix = hit.transform.gameObject;
                    controller.SetState(new StateFixObject(controller));
                }
            }
        }

        if (Time.time - timeStartedPatrol >= controller.timeToPatrolBeforeTask) {
            int rand = Random.Range(0, 2);
            if (rand == 0) controller.SetState(new StateSleep(controller));
            else if (rand == 1) controller.SetState(new StateWork(controller));
            else if (rand == 2) controller.SetState(new StateWork(controller));
            else controller.SetState(new StateWork(controller));
        }
    }

    public override void Act(){
        float dist = Vector3.Distance(controller.transform.position, controller.currentDestination.position);
        if (dist < 3f){
            controller.currentDestination = controller.RandomDestination();
        }

        controller.agent.destination = controller.currentDestination.position;
    }

    public override void OnStateExit(){

    }
}
