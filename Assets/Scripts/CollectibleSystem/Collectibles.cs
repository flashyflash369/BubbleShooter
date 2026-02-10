using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    ///<summary> Base class for all collectibles in the game. Contains common properties and methods that can be shared across different types of collectibles. 
    /// </summary>
    
     [Header("Soap Values")]
    [SerializeField] public int CollectibleLevel = 1;
    [SerializeField] public int HealthAdd = 1;

    public virtual void Collect(string statName, int statValue)
    {
        ///<summary> Collect a collectible with the stat name for refernce and the stat value to apply to the player. 
        /// This method can be called by any collectible type to apply its effects to the player.
        /// </summary>
        
        Debug.Log($"Collecting {statName} with value {statValue}...");
        // Apply effects
        switch (statName)
        {
            case "SoapLevel":
                PlayerStats.instance.SoapLevel += statValue;
                break;
            case "Health":
                PlayerStats.instance.Health += statValue;
                break;
        }
    }

    public virtual void Destroy()
    {
        ///<summary> Destroy the collectible object. 
        /// This method can be overridden by specific collectible types if they have unique destruction behavior (e.g., playing an animation, spawning particles, etc.).
        /// </summary>
        
        //for now just disable it.
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
