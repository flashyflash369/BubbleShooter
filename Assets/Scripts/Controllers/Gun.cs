using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public GameObject firePos;
    public float firerate;
    private float resetFirerate;
    public float bulletSpeed;
    private Vector2 bulletDir;
    private Transform bulletAngle;
   // public ObjectPool objectPool;
    public enum Weapontype
    {
        GrenadeLauncher,
        gun,
        bomb,
        machineGun
    }
    public Weapontype weapontype;
    void Start()
    {
        resetFirerate = firerate;
    }   
    void Update(){ 
        
         if(firerate>0)firerate-= Time.deltaTime*10;
          WeaponState();
         }
    void FixedUpdate()
    {
       
    }

    ///Weapon State
    void WeaponState()
    {
        if(weapontype == Weapontype.gun)
        {
            if(Input.GetMouseButton(0))
            {
                if(firerate<0)
                { 
                    //Fire(); 
                    //firerate=resetFirerate;
                }  
            }
              if(Input.GetMouseButtonDown(0))
            {
                if(PlayerStats.instance.SoapLevel<=0){return;}
                if(firerate<0)
                { 
                    Fire(); 
                    firerate=resetFirerate;
                }  
                
            }
          
            //Change Weapon
        }   
        else if(weapontype == Weapontype.bomb)
        {


            //Change Weapon
        }             
    }


    //Actions
    void Fire()
    {
            Debug.Log("Fire");
            EventSystem.TriggerPlayShoot();
            GameObject spawnBullet = ObjectPool.instance.GetObject("Bullet",firePos.transform);
            PlayerStats.instance.SoapLevel-=1;
    }

}
