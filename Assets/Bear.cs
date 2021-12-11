using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    private void Start()
    {
        //if the pickUps list doesn't contain this pickup
        if (!StateMachine.bears.Contains(this.gameObject))
            //add it to the list
            StateMachine.bears.Add(this.gameObject);
    }
}
