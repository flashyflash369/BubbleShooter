using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject targetObject; // The GameObject to trigger
    public float destroyDelay = 2.0f; // Time in seconds before the object is destroyed

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object has the "Player" tag
        {
            Debug.Log("Trigger entered by Player!");

            // Toggle the target object's active state
            if (targetObject != null)
            {
                targetObject.SetActive(!targetObject.activeSelf);
            }

           // Schedule destruction after the display duration
                Destroy(targetObject, destroyDelay);
            }

            // Optionally destroy the trigger object itself
            Destroy(gameObject);
    }
}
