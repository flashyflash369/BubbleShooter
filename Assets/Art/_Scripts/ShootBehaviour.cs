using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletAnchor;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Bullet Fired");
            GameObject bullet = Instantiate(bulletPrefab, bulletAnchor.position, Quaternion.identity );
            bullet.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            Destroy(bullet, 2f);
        }
    }
}
