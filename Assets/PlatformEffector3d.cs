using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformEffector3d : MonoBehaviour
{
  
 void OnTriggerEnter(Collider other)
  {
    if(gameObject.name == "UpTrigger" && other.tag == "grounded")
    {
        BoxCollider box = other.GetComponent<BoxCollider>();
        if(box!= null)
        {
            box.isTrigger = true;
        }
    }
    else if(gameObject.name == "DownTrigger" && other.tag == "grounded")
    {
        BoxCollider box = other.GetComponent<BoxCollider>();
        if(box!= null)
        {
            box.isTrigger = false;
        }
    }
  }

}
