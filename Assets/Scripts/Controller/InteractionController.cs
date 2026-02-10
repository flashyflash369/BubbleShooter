using UnityEngine;
using System;

public class InteractionController : MonoBehaviour
{
    public PlayerController player;
    public static event Action<Collectibles> OnCollectibleInteract;
    public static event Action<GameObject> OnGenericInteract;

    private void OnTriggerEnter(Collider other)
    {
        // 🔹 Collectible interaction
        if (other.TryGetComponent(out Collectibles collectible))
        {
            OnCollectibleInteract?.Invoke(collectible);
            return;
        }

        // 🔹 Fallback (doors, NPCs, etc.)
        OnGenericInteract?.Invoke(other.gameObject);
    }
}
