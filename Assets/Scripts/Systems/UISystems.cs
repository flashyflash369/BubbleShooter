using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystems : MonoBehaviour
{
    public static UISystems instance;
   //----------------OnMenu-----------///



    //----------------InGameUI-----------///

    //HealthSlider
    public Slider healthBar;
    //SoapLevelSlider
    public Slider SoapLevel;

    void Awake() => instance = instance == null ? this : instance.Also(obj=> Destroy(this));


    
}
