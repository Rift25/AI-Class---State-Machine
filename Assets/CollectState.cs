using UnityEngine;
using UnityEngine.AI;

public class CollectState : INPCState
{
    public INPCState DoState(StateMachine npc)
    {
        //if the navAgent is null
        if (npc.navAgent == null)
            //make sure its there
            npc.navAgent = npc.GetComponent<NavMeshAgent>();
        //tells the npc to move to the collect 
        DoCollect(npc);
        //if the pickUp isn't alive
        if (!npc.pickUpTarget.activeSelf)
            //go back to wander state
            return npc.wanderState;
        else
            //otherwise set the state to be collect state
            return npc.collectState;
    }

    private void DoCollect(StateMachine npc)
    {
        //if the npcs destination isn't the pickUps position
        if (npc.navAgent.destination != npc.pickUpTarget.transform.position)
            //move it to its location
            npc.navAgent.SetDestination(npc.pickUpTarget.transform.position);
    }
}