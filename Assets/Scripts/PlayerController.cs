using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour 
{
    public GameObject gunObject;
    public GameObject gunPlaceholder;
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
    private float dashSpeed = 0;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
   // private float DashDamping;
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
  //  private float resetDash;
     private Vector3 mousePos;
//    private Animator anim;
    enum PlayerState
    {
        Move,
        Jump,
        idle
    }
    
  
    private PlayerState Currentstate;


    // Start is called before the first frame update
       void Start()
    {
        rb = GetComponent<Rigidbody>();
      //  resetDash = dashSpeed;
        Currentstate = PlayerState.idle;
         //Physics.gravity = new Vector3(0, -1*Mathf.Abs(gravityScale), 0);
      Application.targetFrameRate = 60;
       // anim = GetComponent<Animator>();
    }

//[AtomicCommand(name: "TestConsole")]
private void Test()
{
 Debug.Log("Hello Console");
}

    // Update is called once per frame
    void Update()
    {
        movePositions = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        States();
        AimCursor();

    }
    void FixedUpdate()
    {
        FlipXAxis();
        PhyscisUpdate();
        isGrounded = Physics.CheckSphere(groundedPos.position,groundRadius,layerMask);

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
        //    anim.SetBool("Move",true);    
        }
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Currentstate = PlayerState.Jump;
        }
    }
    else if(Currentstate == PlayerState.Move)
    {

       // Move();
        //Transition
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Currentstate = PlayerState.Jump;
          //  trail.SetActive(true);
         //   dashHit.SetActive(true);
         //   dashHit.transform.position = transform.position;
         //   dashEffect.SetActive(true);
            
           
        
        }
        else if(movePositions.x == 0 && movePositions.y == 0)
        {
             Currentstate = PlayerState.idle;
        //     anim.SetBool("Move",false);
           
        }
    }
    else if(Currentstate == PlayerState.Jump)
    {

        //ActivaDash
        //Transition
        if(!isGrounded)
        {
            Currentstate = PlayerState.Move;
        //    dashSpeed = resetDash;
        //    trail.SetActive(false);
        //    dashEffect.SetActive(false);
        //    dashHit.SetActive(false);
        }
    }
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

    void Jump()
    {

        transform.Translate(jumpForce*Time.fixedDeltaTime*Vector2.up);
    }
    void Move()
    {
       
        transform.Translate(new Vector3(speed*Time.fixedDeltaTime*movePositions.x,0,0));
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
    void ActivateDash()
    {  
          Vector2 dashDirection = movePositions;
          rb.velocity = dashDirection*dashSpeed*Time.deltaTime;
        //  dashSpeed-=Time.deltaTime*Mathf.Exp(DashDamping);
    }
    
  #endregion

  
}
