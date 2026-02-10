using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour 
{
    public GameObject gunObject;
    public GameObject gunPlaceholder;
    public Transform firePos;
  //  public GameObject dashHit;
 //   public GameObject dashEffect;
    public float angleOffset;
 //   [SerializeField]
    private float cursorDistance;
    [SerializeField]
    private float speed;
    [SerializeField] private float gravityScale;
    [SerializeField]
    [Tooltip("")]
    private float recoilForce = 0;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float DashDamping;
  //  [SerializeField]
    //private GameObject particle;
  //  [SerializeField]
 //   private GameObject trail;
    private Vector2 movePositions;
    private bool isGrounded = false;
    [SerializeField] private Transform groundedPos;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float groundRadius;
    [SerializeField] private float jumpForce;
    private float resetRecoilForce;
     private Vector3 mousePos;
     public Material flashMat;
     private float speedReset;

     private bool isFrozen = false;
    
     
//    private Animator anim;
    enum PlayerState
    {
        Move,
        Jump,
        idle
    }
    
    public GameObject[] mouths;
    GameObject currentMouth;
    private PlayerState Currentstate;


    // Start is called before the first frame update
       void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        currentMouth = mouths[0];
        currentMouth.SetActive(true);
        speedReset = speed;
        
          resetRecoilForce = recoilForce;
          resethitForce = hitForce;
          hitForce = 0;
         Currentstate = PlayerState.idle;
         Application.targetFrameRate = 60;
       //  Physics.gravity = new Vector3(0, -1*Mathf.Abs(gravityScale), 0);
    
       // anim = GetComponent<Animator>();
    }

    public void FreezePlayer()
    {
        isFrozen = true;

        // Stop motion immediately
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void UnfreezePlayer()
    {
        isFrozen = false;
    }

    //[AtomicCommand(name: "TestConsole")]
    private void Test()
    {
    Debug.Log("Hello Console");
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen) return;

        movePositions = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        FlipXAxis();
        States();
        AimCursor();
//        Debug.Log(Currentstate);
        
    }
    void FixedUpdate()
    {
        if (isFrozen)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }
       //PhyscisUpdate();
       isGrounded = Physics.CheckSphere(groundedPos.position,groundRadius,layerMask);
       if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {Jump();}
       

       Move();
       if(Input.GetMouseButtonDown(0)){ if(PlayerStats.instance.SoapLevel<=0){return;}recoilForce = resetRecoilForce ;Recoil();}
      HitbackForce();

    }
    /// <summary>
/// Player States
/// </summary>
#region PlayerState
void States()
{
     if(Currentstate == PlayerState.idle)
    {
        
       // Move();
        //Transition
        if(movePositions.x != 0 || movePositions.y != 0)
        {
            Currentstate = PlayerState.Move;
            MouthSwitch(mouths[0],mouths[2]);
        //    anim.SetBool("Move",true);    
        }
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Currentstate = PlayerState.Jump;
            MouthSwitch(mouths[0],mouths[1]);

        }
    }
    else if(Currentstate == PlayerState.Move)
    {
     
       // Move();
        //Transition
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Currentstate = PlayerState.Jump;
               MouthSwitch(mouths[2],mouths[1]);
          //  trail.SetActive(true);
         //   dashHit.SetActive(true);
         //   dashHit.transform.position = transform.position;
         //   dashEffect.SetActive(true);
        
        }
        else if(movePositions.x == 0 && movePositions.y == 0)
        {
              MouthSwitch(mouths[2],mouths[0]);
              Currentstate = PlayerState.idle;
        //     anim.SetBool("Move",false); 
        }
    }
    else if(Currentstate == PlayerState.Jump)
    {
        
        //ActivaDash
        //Transition
        if(isGrounded)
        {
            Invoke("TimerMoveToJump",0.1f);
        //    dashSpeed = resetDash;
        //    trail.SetActive(false);
        //    dashEffect.SetActive(false);
        //    dashHit.SetActive(false);
        }
    }
}
void TimerMoveToJump()
{
     Currentstate = PlayerState.idle; 
    MouthSwitch(mouths[1],mouths[0]);
     
}
void PhyscisUpdate()
{
    if(Currentstate == PlayerState.idle)
    {
        Move();
    }
    else if(Currentstate == PlayerState.Move)
    {
        Move();
    }
    else if(Currentstate == PlayerState.Jump)
    {
        //ActivateDash();
        Jump();
    }

}


#endregion

#region Actions

    void MouthSwitch(GameObject current, GameObject next)
    {
        current.SetActive(false);
        next.SetActive(true);
    }
    void Jump()
    {
        rb.linearVelocity =jumpForce*Time.deltaTime*Vector2.up*6;
       //transform.Translate(jumpForce*Time.deltaTime*Vector2.up);
    }
    void Move()
    {
         rb.linearVelocity = new Vector3(movePositions.x*speed*Time.deltaTime,rb.linearVelocity.y,rb.position.z);
    }

    void FlipXAxis()
    {
        
        Vector3 flipDir = mousePos - rb.position;
        if ((movePositions.x < 0 && flipDir.x<0)||flipDir.x<0)
        {
            transform.localScale =  transform.localScale.x>0 ? transform.localScale:new Vector3(-1*transform.localScale.x,transform.localScale.y, transform.localScale.z); 
              gunObject.transform.localScale = new Vector3( gunObject.transform.localScale.x<0?gunObject.transform.localScale.x*-1:gunObject.transform.localScale.x,
            gunObject.transform.localScale.y,gunObject.transform.localScale.z);
           
        }
        else if ((movePositions.x > 0 && flipDir.x>0)|| flipDir.x>0)
        {
            transform.localScale = transform.localScale.x<0 ?  transform.localScale  :new Vector3(-1*transform.localScale.x,transform.localScale.y, transform.localScale.z);
          

             gunObject.transform.localScale = new Vector3( gunObject.transform.localScale.x<0?gunObject.transform.localScale.x:gunObject.transform.localScale.x*-1,
            gunObject.transform.localScale.y,gunObject.transform.localScale.z);
        }
    }
    
    void FlipXAxisGun()
    {
        
    }
    void FlipYAxisByAngle(float angle)
    {
        //Debug.Log(angle);
        if (angle < 110)
        {
            gunObject.transform.localScale = new Vector3(gunObject.transform.localScale.x,
            gunObject.transform.localScale.x<0? gunObject.transform.localScale.x*-1: gunObject.transform.localScale.x ,gunObject.transform.localScale.z);
        }
        else if (angle < 270)
        {
            gunObject.transform.localScale = new Vector3(gunObject.transform.localScale.x,-1* gunObject.transform.localScale.x,  gunObject.transform.localScale.z);
        }
        else if(angle > 270){
             gunObject.transform.localScale = new Vector3(gunObject.transform.localScale.x,
            gunObject.transform.localScale.x<0? gunObject.transform.localScale.x*-1: gunObject.transform.localScale.x ,gunObject.transform.localScale.z);
        }

        
    gunObject.transform.localScale =new Vector2(gunObject.transform.localScale.x,
    gunObject.transform.localScale.x<0? gunObject.transform.localScale.y*-1:gunObject.transform.localScale.y) ;
        
    }
    void AimCursor()
    {
        
          Vector3 mouseScreenPos = Input.mousePosition;

        // Create a ray from the camera to the mouse position on the screen
        Ray ray = Camera.main.ScreenPointToRay(mouseScreenPos);

        RaycastHit hit;

        // Perform the raycast to see if it hits something in the world
        if (Physics.Raycast(ray, out hit))
        {
            // If the raycast hits an object, get the point in world space
           // Vector3 worldMousePos = hit.point;


        Vector3 mouseDir= new Vector3();
        mousePos = hit.point;
        mouseDir = mousePos-gunPlaceholder.transform.position;
        //Debug.Log(mousePos);
       // gunObject.transform.position = (mousePos.normalized*cursorDistance)+gunPlaceholder.transform.position;
        mouseDir.z = 0;
        float radians = Mathf.Atan2(mouseDir.normalized.y,mouseDir.normalized.x);
        gunObject.transform.eulerAngles = new Vector3(gunObject.transform.rotation.x,gunObject.transform.rotation.y,(Mathf.Rad2Deg* radians)-angleOffset);
        FlipYAxisByAngle(gunObject.transform.eulerAngles.z);

          
        }
    
       
        
    }
    public void Recoil()
    {  
          Vector3 dashDirection = transform.position-firePos.position;
          dashDirection.z = 0;
          //rb.AddForce(dashDirection*dashSpeed*Time.deltaTime*6,ForceMode.Impulse);
          rb.linearVelocity =dashDirection*recoilForce*Time.deltaTime*6 ;
          recoilForce-=Time.deltaTime*DashDamping;
          if(recoilForce<0){recoilForce = resetRecoilForce;}
    }
    
  #endregion

    [SerializeField] private float hitForce;
    [SerializeField] private float dampHitForce = 1000;
     private Vector3 hitPos;
   private float resethitForce;
    

    private void OnTriggerEnter(Collider other)
    {
        IDamagable OnPlayerHit = other.gameObject.GetComponent<IDamagable>();
        if(OnPlayerHit!=null)
        {
            OnPlayerHit.OnHit();
            flashMat.SetFloat("_OnFlash",1);
            Invoke("FlashOff",0.1f);
            hitForce = resethitForce;
            hitPos = other.gameObject.transform.position;
          
          

          
        }
    }
    public void FlashOff()=> flashMat.SetFloat("_OnFlash",0);
    public void HitbackForce()
    {

        if(hitForce>0) 
        {
            speed = 0;
            Vector3 direction = hitPos-transform.position;
            direction.z=0;
               rb.AddForce(Vector2.right*-direction.x*hitForce*Time.deltaTime*100,ForceMode.Acceleration);
               rb.AddForce(Vector2.up*-direction.y*hitForce/100*Time.deltaTime*100,ForceMode.Acceleration);
              hitForce-=Time.deltaTime*dampHitForce;
        }
        else
        {
            speed = speedReset;
        }
       
    }
  
}
