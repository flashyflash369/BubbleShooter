using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soap : Collectibles
{

    //Properties
    [SerializeField] private int SoapLevel;
    [SerializeField] private int HealthAdd;
    [SerializeField] private GameObject soapObject;

    private void OnTriggerEnter(Collider other) 
    {
      if (other.CompareTag("Player"))
      {
       //Add to SoapLevel and Increase Health
       Debug.Log("Added 1 to Saoplevel");
       Debug.Log("Added 1 to Healthlevel");

       EventSystem.TriggerCollectible(SoapLevel, HealthAdd); // Raise the event, passing the storyPointIndex
       PlayerStats.instance.SoapLevel+=SoapLevel;
       PlayerStats.instance.Health+=HealthAdd;
       //Destroy
       //ObjectPool.instance.ReturnObject("Soap",gameObject);
       Destroy(gameObject);
       PlayerStats.instance.score+=1;
       UISystems.instance.SoapPoints.text = PlayerStats.instance.score.ToString();
      }
    }
   
}
