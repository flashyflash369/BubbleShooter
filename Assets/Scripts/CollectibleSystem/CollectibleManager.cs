using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    private void OnEnable()
    {
        InteractionController.OnCollectibleInteract += HandleCollect;
    }

    private void OnDisable()
    {
        InteractionController.OnCollectibleInteract -= HandleCollect;
    }

    private void HandleCollect(Collectibles collectible)
    {
        Debug.Log("Collecting soap...");

        collectible.Collect("SoapLevel", collectible.CollectibleLevel);
        collectible.Collect("Health", collectible.HealthAdd);
        collectible.Destroy();

    }
}
