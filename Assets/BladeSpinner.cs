using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSpinner : MonoBehaviour,IDamagable
{
    [SerializeField] private float spinSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward*spinSpeed*Time.deltaTime);
    }

    void IDamagable.OnHit()
    {
         PlayerStats.instance.Health-=1;
    }
}
