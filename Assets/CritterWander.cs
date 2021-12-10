using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CritterWander : MonoBehaviour
{
    //NavMeshAgent
    private NavMeshAgent navAgent;
    //what the characters next location is when it changed its state
    private Vector3 nextLocation;
    [SerializeField]
    //how long the unit travels before it changes direction again
    private float wanderDistance = 15f;
    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {
        //make the critter move to the next location
        nextLocation = this.transform.position;
        //get the NavMesh for the critter
        navAgent = this.GetComponent<NavMeshAgent>();
        //if the critters list doesn't contain this critter
        if (!StateMachine.critters.Contains(this.gameObject))
            //add it to the list
            StateMachine.critters.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //set the critters state to be wander
        DoWander();
    }

    private void DoWander()
    {
        //if the npc has reached its destination
        if (navAgent.remainingDistance < 1f)
        {
            //get a random location
            Vector3 random = Random.insideUnitSphere * wanderDistance;
            //the location can't be above or below us
            random.y = 0f;
            //make the critters next location be randomized
            nextLocation = this.transform.position + random;
            //if the next location we want to travel to is walkable
            if (NavMesh.SamplePosition(nextLocation, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            {
                //make that next location be the NavMeshes next location
                nextLocation = hit.position;
                //set the destination to be the critters next location to travel to
                navAgent.SetDestination(nextLocation);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the critter collides with an object that has a tag NPC
        if (other.tag == "NPC")
            //destroy itself
            DoDestroy();
    }

    public void DoDestroy()
    {
        //remove the critter from the list
        StateMachine.critters.Remove(this.gameObject);
        //and set it to false
        this.gameObject.SetActive(false);
    }
}
