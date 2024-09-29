using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public GameObject scanner;
    public ControllerState currentState;
    public List<Transform> destinations = new List<Transform>();
    public NavMeshAgent agent;
    public Transform currentDestination;
    public GameObject objectToFix;
    public float timeToFix = 3f;
    public float timeToSleep = 5f;
    public float timeToAnger = 5f;
    public float timeToWork = 5f;
    public float timeBetweenFixToAnger = 5f;
    public float timeToPatrolBeforeTask;
    public bool triedToWork;
    public GameObject workSpot;
    public GameObject sleepSpot;
    public Breakable computer;
    public Material angerMaterial;
    public Material calmMaterial;
    public float lastFixTime;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentDestination = RandomDestination();
        SetState(new StatePatrol(this));
    }

    public Transform RandomDestination(){
        if (destinations.Count > 0){
            int rd = Random.Range(0, destinations.Count);
            return destinations[rd];
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.CheckTransitions();
        currentState.Act();
    }

    public void SetState(ControllerState state){
        if (currentState != null) {
            currentState.OnStateExit();
        }

        currentState = state;

        if (currentState != null){
            currentState.OnStateEnter();
        }
    }
}
