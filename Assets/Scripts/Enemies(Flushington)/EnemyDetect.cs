using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{


    private void OnTriggerEnter(Collider other) {
          if (other.CompareTag("Player"))
          {
            EventSystem.TriggerEnemyDetect(other.transform);
          }
    }
}
