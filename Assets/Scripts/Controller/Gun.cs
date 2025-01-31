using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
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

    [SerializeField] private float rayRadius;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask layerMask;
    public  RaycastHit hitInfo;
    public bool onBubbleHit;
    public bool lockBubble;
    [SerializeField] private float swingForce;
     [SerializeField] private float pullForce;
     [SerializeField] private float swingMaxDistance;
    Vector2 direction;
     public Vector2 pullDir ;
    private PlayerController playerController;
   // public ObjectPool objectPool;

   private LineRenderer lineRenderer;
   public Vector3 target; 
    private OutlineActivate outlineActivate = new OutlineActivate();
    public enum Weapontype
    {
       
        gun,
        glapplingGun
       
    }
    public Weapontype weapontype;
    void Start()
    {
        resetFirerate = firerate;
        hitInfo = new RaycastHit();

        ///Set line renderer 
        ///
        playerController = GetComponent<PlayerController>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;  // Two points: start and end
      
    }   
    void Update(){ 
        direction =  firePos.transform.position-transform.position;
         if(firerate>0)firerate-= Time.deltaTime*10;
          WeaponState();
          WeaponSwitch();
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
                PlayerStats.instance.SoapLevel-=1;
                if(PlayerStats.instance.SoapLevel<=0){return;}
                if(firerate<0)
                { 
                  //  Debug.Log( "Soap Level"+PlayerStats.instance.SoapLevel);
                    Fire(); 
                    firerate=resetFirerate;
                }  
                
            }
          
            //Change Weapon
        }   
        else if(weapontype == Weapontype.glapplingGun)
        {
          
         if(lockBubble == false){
        if (Physics.Raycast(transform.position,direction,out hitInfo,rayDistance,layerMask))
        {
            // If we hit something, print the name of the object

//            Debug.Log("Hit: " + hitInfo.collider.gameObject.name);
           
            
            if(outlineActivate!=null){ outlineActivate.isActive =false;}
             outlineActivate = hitInfo.collider.gameObject.GetComponent<OutlineActivate>();
            if(outlineActivate!=null)
            {
                outlineActivate.isActive =true;
            }
             onBubbleHit = true;
            target = hitInfo.collider.gameObject.transform.position;     
            if(Input.GetMouseButtonDown(0)){
                lockBubble = true;
             
                }
        
        }
        else
        {
            onBubbleHit = false;
            if(outlineActivate!=null){ outlineActivate.isActive =false;}
            // If nothing was hit
//            Debug.Log("No hit");
        }
         }

            //Change Weapon
        }        
              Debug.DrawRay(transform.position, direction*rayDistance, Color.red);  

         if(Input.GetMouseButtonDown(0)){pullDir = target - firePos.transform.position;}
        if(Input.GetMouseButton(0) && lockBubble)
        {
            lineRenderer.SetPosition(0, firePos.transform.position);  // Start point (gun position)
            lineRenderer.SetPosition(1, target);  // End point (target position)
            

         
              
        }
        if(Input.GetMouseButtonUp(0)){
          
             lineRenderer.SetPosition(0, Vector3.zero);  // Start point (gun position)
            lineRenderer.SetPosition(1, Vector3.zero);
            lockBubble = false;
          if(outlineActivate!=null){ outlineActivate.isActive =false;}
            }
    }

    void WeaponSwitch()
    {
        if(Input.GetMouseButtonDown(1))
        {
            weapontype = (Weapontype)(((int)weapontype + 1) % System.Enum.GetValues(typeof(Weapontype)).Length);

        }
    }

    //Actions
    
    void Fire()
    {
            Debug.Log("Fire");
            EventSystem.TriggerPlayShoot();
            GameObject spawnBullet = ObjectPool.instance.GetObject("Bullet",firePos.transform);
        
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Draw a sphere at the start position of the SphereCast
      //  Gizmos.DrawRay(hitInfo);
    } 


    public void glapplingPhysics()
    {
           if(playerController!=null)
            {
            Rigidbody rb = playerController.GetComponent<Rigidbody>();
            ///Apply Force in Direction

           // rb.velocity = dir.normalized*pullForce*Time.deltaTime*6;
            rb.AddForce(pullDir.normalized*pullForce*Time.deltaTime*6,ForceMode.VelocityChange);
            ///max line Distance
        


            ///Swing 


            }

            
    }

}
