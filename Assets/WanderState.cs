using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
//https://onewheelstudio.com/blog/2020/6/16/the-state-pattern
public class WanderState : INPCState
{
    public INPCState DoState(StateMachine npc)
    {
        //if the navAgent is null
        if (npc.navAgent == null)
        {
            //give it a new location to travel to
            npc.nextLocation = npc.transform.position;
            //and make sure its there
            npc.navAgent = npc.GetComponent<NavMeshAgent>();
        }
        //set the npcs state to be wander
        DoWander(npc);
        //if it sees the critter change the state to attack
        if (CanSeeCritter(npc))
            return npc.attackState;
        //if it sees the pickup change its state to collect
        else if (CanSeePickUp(npc))
            return npc.collectState;
        else
            //otherwise just do wander state
            return npc.wanderState;
    }

    private void DoWander(StateMachine npc)
    {
        //if the npc has reached its destination
        if (npc.navAgent.remainingDistance < 1f)
        {
            //get a random location
            Vector3 random = Random.insideUnitSphere * npc.wanderDistance;
            //the location can't be above or below us
            random.y = 0f;
            //make the npcs next location be randomized
            npc.nextLocation = npc.navAgent.transform.position + random;
            //if the next location we want to travel to is walkable
            if (NavMesh.SamplePosition(npc.nextLocation, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            {
                //make that next location be the NavMeshes next location
                npc.nextLocation = hit.position;
                //set the destination to be the npcs next location to travel to
                npc.navAgent.SetDestination(npc.nextLocation);
            }
        }
    }

    private bool CanSeePickUp(StateMachine npc)
    {
        //loop through all the pickups in the list
        foreach (GameObject pickup in StateMachine.pickUps)
        {
            //get the distance between the pickup and the npc
            float distance = (pickup.transform.position - npc.transform.position).magnitude;
            //if the distance between the pickup and the npc is less than the npcs pickup distance
            if (distance < npc.pickUpDistance)
            {
                //get the direction towards the pickup
                Vector3 direction = pickup.transform.position - (npc.transform.position + Vector3.up);
                //make a ray that tells us where the pickup is
                Ray ray = new Ray(npc.transform.position + Vector3.up, direction);
                //make it blue
                Debug.DrawRay(npc.transform.position + Vector3.up, direction, Color.blue);
                //if the raycast detects the pickup
                if (Physics.Raycast(ray, out RaycastHit hit, npc.pickUpDistance))
                {
                    //print out that we have found it
                    Debug.Log("I hit" + hit.collider.name);
                    //if the npcs collider collides with the pickups collider
                    if (hit.collider.gameObject == pickup)
                    {
                        //make the pickup the npcs next target
                        npc.pickUpTarget = pickup; //a little yucky but keeps the base class clean
                        //returns true and transitions to CollectState
                        return true;
                    }
                }
            }
        }
        //otherwise if we don't see the pickup stay in WanderState
        return false;
    }

    private bool CanSeeCritter(StateMachine npc)
    {
        //loop through all the critters in the list
        foreach (GameObject critter in StateMachine.critters)
        {
            //get the distance between the critter and the npc
            float distance = (critter.transform.position - npc.transform.position).magnitude;
            //if the distance between the critter and the npc is less than the npcs pickup distance
            if (distance < npc.pickUpDistance)
            {
                //get the direction towards the critter
                Vector3 direction = critter.transform.position - (npc.transform.position + Vector3.up);
                //make a ray that tells us where the critter is
                Ray ray = new Ray(npc.transform.position + Vector3.up, direction);
                //make it red
                Debug.DrawRay(npc.transform.position + Vector3.up, direction, Color.red);
                //if the raycast detects the critter
                if (Physics.Raycast(ray, out RaycastHit hit, npc.pickUpDistance))
                {
                    //print out that we have found it
                    Debug.Log("I hit" + hit.collider.name);
                    //if the npcs collider collides with the critters collider
                    if (hit.collider.gameObject == critter)
                    {
                        //make the critter the npcs next target
                        npc.critterTarget = critter;
                        //returns true and transitions to AttackState
                        return true;
                    }
                }
            }
        }
        //otherwise if we don't see the critter stay in WanderState
        return false;
    }
}
