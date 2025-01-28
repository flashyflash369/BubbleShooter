using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour,IDamagable
{
    public float Damage;
    // Start is called before the first frame update
   void IDamagable.OnHit()
    {
         PlayerStats.instance.Health-= Damage;
    }
}
