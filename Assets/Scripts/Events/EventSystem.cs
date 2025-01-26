using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSystem : MonoBehaviour
{
    static public EventSystem instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //NARRATION
    // Passes the storyPointIndex to the NarrationSystem
    public static event Action<int> OnStoryPointTriggered;
    
    // StoryPoint
    public static void TriggerStoryPoint(int storyPointIndex)
    {
        OnStoryPointTriggered?.Invoke(storyPointIndex);
    }

    // Event to destroy a story point by index
    public static Action<int> OnStoryPointDestroy;



    //COLLECTIONS
    // Passes the SoapValue to the PlayerSoapLevel.
    public static event Action<int, int> OnCollectibleTriggered;

    // Collectibles
    public static void TriggerCollectible(int collectibleSoapValue, int collectibleHealthValue)
    {
        OnCollectibleTriggered?.Invoke(collectibleSoapValue, collectibleHealthValue);
    }

    //ENEMY DETECT
    // 
    public static event Action<Transform> OnEnemyDetectTriggered;

    // Player detected by enemy
    public static void TriggerEnemyDetect(Transform chasePoint)
    {
        OnEnemyDetectTriggered?.Invoke(chasePoint);
    }

     //On Enemy Hit
    // 
    public  static event Action OnEnemyHit;

    // Player detected by enemy
    public static void TriggerOnEnemyHit()
    {
       OnEnemyHit?.Invoke();
    }

    public  static event Action Playshoot;

    // Player detected by enemy
    public static void TriggerPlayShoot()
    {
       Playshoot?.Invoke();
    }




}
