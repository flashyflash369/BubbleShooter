using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveForward : MonoBehaviour
{

    [SerializeField] private float speed;

    void Update()
    {
        this.transform.Translate(Vector3.right * speed * Time.deltaTime);    
    }
}
