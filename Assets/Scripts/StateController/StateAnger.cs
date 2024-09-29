using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateAnger : ControllerState
{
    public StateAnger(StateController controller) : base(controller) { }

    private float timeStartedAnger;
    private bool isAnger;
    private bool doneAnger;

    public override void OnStateEnter(){
        Debug.Log("Entered anger state.");
        controller.GetComponent<NavMeshAgent>().isStopped = true;
        doneAnger = false;
        controller.GetComponent<Renderer>().material = controller.angerMaterial;
    }

    public override void CheckTransitions(){
        //If it finishes working
        if (doneAnger) 
        {
            controller.GetComponent<Renderer>().material = controller.calmMaterial;
            controller.SetState(new StateSleep(controller));
        }
    }

    public override void Act(){
                if (!isAnger){
                    timeStartedAnger = Time.time;
                    Debug.Log("Started anger at " + timeStartedAnger);
                    isAnger = true;
                }
                else{
                    Debug.Log("Is angry...");
                    controller.agent.isStopped = true;
                    if (Time.time - timeStartedAnger >= controller.timeToAnger){
                        Debug.Log("Finished anger.");
                        controller.agent.isStopped = false;
                        doneAnger = true;
                    }
                }
        
    }

    public override void OnStateExit(){
        controller.agent.isStopped = false;
    }

}