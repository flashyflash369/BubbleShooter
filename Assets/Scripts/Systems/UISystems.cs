using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public TextMeshProUGUI SoapPoints;

     public TextMeshProUGUI FinalSoapPoints;


    void Awake() => instance = instance == null ? this : instance.Also(obj=> Destroy(this));


    
}
