using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//an interface State with only one function 
public interface INPCState
{
    //this function runs all of the states behaviours (Wander, Attack, Collect)
    //the input parameter is there so that the npc can pass in a reference to itself
    //so that the state can have access to any of the npcs variables
    INPCState DoState(StateMachine npc);
}
