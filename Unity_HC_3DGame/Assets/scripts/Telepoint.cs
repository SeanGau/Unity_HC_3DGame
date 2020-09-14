using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telepoint : MonoBehaviour
{
    public Transform target;
    public bool justTrans = false;
    private void OnTriggerEnter(Collider other)
    {
        target.gameObject.GetComponent<Telepoint>().justTrans = true;
        if(other.name == "Hero" && !justTrans)
        {
            other.transform.position = target.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Hero")
        {
            justTrans = false;
        }
    }

}
