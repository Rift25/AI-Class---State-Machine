using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void Start()
    {
        //if the pickUps list doesn't contain this pickup
        if (!StateMachine.pickUps.Contains(this.gameObject))
            //add it to the list
            StateMachine.pickUps.Add(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the tag is NPC
        if (other.tag == "NPC")
        {
            //remove the pickup from the list
            StateMachine.pickUps.Remove(this.gameObject);
            //and set it to false
            this.gameObject.SetActive(false);
        }
    }
}
