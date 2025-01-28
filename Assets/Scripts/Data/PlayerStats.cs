using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
   public static PlayerStats instance; 
   [Range(0,10)]
   public  float Health;
   [Range(0,10)]
   public float SoapLevel;
    
    void Awake() => instance = instance == null ? this : instance.Also(obj=> Destroy(this));

    void Update()
    {
        Health = Mathf.Clamp(Health,0,10);
        SoapLevel = Mathf.Clamp(SoapLevel,0,10);
        UISystems.instance.healthBar.value = Health/10;
        UISystems.instance.SoapLevel.value = SoapLevel/10;
    }
}
