using UnityEngine;
using UnityEngine.AI;
//Helped me implement run away state
//https://www.youtube.com/watch?v=Zjlg9F3FRJs

public class RunState : INPCState
{
    public INPCState DoState(StateMachine npc)
    {
        //if the navAgent is null
        if (npc.navAgent == null)
            //make sure its there
            npc.navAgent = npc.GetComponent<NavMeshAgent>();
        //tells the npc to move to the critter
        RunFromBear(npc);
        foreach (GameObject bear in StateMachine.bears)
        {
            float distance = Vector3.Distance(npc.transform.position, bear.transform.position);

            if (distance > npc.BearDistance)
            {
                return npc.wanderState;
            }
        }
        //if the critter isn't alive
        if (!npc.bearTarget.activeSelf)
            //go back to wander state
            return npc.wanderState;
        else
            //otherwise set the state to be attack state
            return npc.runState; 
    }

    private void RunFromBear(StateMachine npc)
    {
        foreach (GameObject bear in StateMachine.bears)
        {
            float distance = Vector3.Distance(npc.transform.position, bear.transform.position);
            if (distance <= npc.BearDistance)
            {
                Vector3 dirToPlayer = npc.transform.position - bear.transform.position;
                Vector3 newPos = npc.transform.position + dirToPlayer;
                npc.navAgent.SetDestination(newPos);
            }
        }
    }
}
