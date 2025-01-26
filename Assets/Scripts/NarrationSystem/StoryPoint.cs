using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPoint : MonoBehaviour
{
    public int storyPointIndex;

        void OnEnable()
    {
        // Subscribe to the StoryPoint event
        EventSystem.OnStoryPointDestroy += HandleStoryPointDestroy;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        EventSystem.OnStoryPointDestroy -= HandleStoryPointDestroy;
    }

    //Detect Player
    private int OnTriggerEnter(Collider other) {

             if (other.CompareTag("Player"))
             {
               Debug.Log($"Player detected at story point {storyPointIndex}");
               EventSystem.TriggerStoryPoint(storyPointIndex); // Raise the event, passing the storyPointIndex
             }

               return storyPointIndex;

        }


        
        // Handle the destruction of a story point
        // Handle the destroy event
       private void HandleStoryPointDestroy(int index)
       {
        if (index == storyPointIndex)
        {
            Debug.Log($"Destroying StoryPoint with index: {storyPointIndex}");

            // Destroy this GameObject
            Destroy(gameObject);
        }
       }

}
