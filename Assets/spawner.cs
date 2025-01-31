using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public float soapSpanwTime;
    public float spinbladeSpawnTime;
    public GameObject blade;
    public GameObject Soap;
  
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine("SpawnSoap");
         StartCoroutine("SpawnBlade");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDisable()
    {
         StopCoroutine("SpawnSoap");
         StopCoroutine("SpawnBlade");
    
    }

    IEnumerator SpawnSoap()
    {
        while(true)
        {
            GameObject   gameObject  = Instantiate(Soap,new Vector3(Random.Range(-34 , 22),6,-1.35f),Quaternion.identity);
            yield return new WaitForSeconds(soapSpanwTime);
        }
    }
    IEnumerator SpawnBlade()
    {
          while(true)
        {
         GameObject   gameObject = Instantiate(blade,new Vector3(Random.Range(-34 , 22),-1,-1.35f),Quaternion.identity);
         BladeSpinner bladeSpinner = gameObject.GetComponent<BladeSpinner>();
         if(bladeSpinner!=null){bladeSpinner.currentState = BladeSpinner.State.move;}

            yield return new WaitForSeconds(spinbladeSpawnTime);
        }
    }
}
