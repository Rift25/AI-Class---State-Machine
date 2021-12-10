using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StateMachine : MonoBehaviour
{
    [SerializeField]
    //name of the current state
    private string currentStateName;
    //what the current state is
    private INPCState currentState;

    //create a new instace of wander state on start
    public WanderState wanderState = new WanderState();
    //create a new instace of collect state on start
    public CollectState collectState = new CollectState();
    //create a new instace of attack state on start
    public AttackState attackState = new AttackState();
    //create a new instace of run state on start
    public RunState runState = new RunState();
    //NavMeshAgent
    public NavMeshAgent navAgent;
    //what the characters next location is when it changed its state
    public Vector3 nextLocation;
    //the item to collect
    public GameObject pickUpTarget;
    //the target to destroy
    public GameObject critterTarget;
    //how long the unit travels before it changes direction again
    public float wanderDistance = 10f;
    //at what range does the unit see the item to collect or a critter to destroy
    public float pickUpDistance = 17f;
    //create a new list of game objects to collect
    public static List<GameObject> pickUps = new List<GameObject>();
    //create a new list of game objects to destroy
    public static List<GameObject> critters = new List<GameObject>();

    private void OnEnable()
    {
        //set the current state to be wander state on game start
        currentState = wanderState;
    }

    // Update is called once per frame
    void Update()
    {
        //constantly update what the current state is incase it changes
        currentState = currentState.DoState(this);
        //print out the current state
        currentStateName = currentState.ToString();
    }
}