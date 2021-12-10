using UnityEngine;
using UnityEngine.AI;

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
        //if the critter isn't alive
        if (!npc.critterTarget.activeSelf)
            //go back to wander state
            return npc.wanderState;
        else
            //otherwise set the state to be attack state
            return npc.runState;
    }

    private void RunFromBear(StateMachine npc)
    {
        //if the npcs destination isn't the critters position
        if (npc.navAgent.destination != npc.critterTarget.transform.position)
            //move it to its location
            npc.navAgent.SetDestination(-npc.critterTarget.transform.position);
    }
}
