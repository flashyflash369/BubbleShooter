using UnityEngine;

public class Soap : Collectibles
{
  [Header("Prefab to Spawn")]
  [SerializeField] private GameObject soapPrefab;

  private  GameObject soapInstance ;

  void Start()
  {
    // 🔹 Instantiate a COPY at this soap's position
     soapInstance = Instantiate(soapPrefab, transform.position, transform.rotation);
  }
}
