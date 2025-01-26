using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseBubble : MonoBehaviour
{
   [SerializeField] protected float floatingForce =100;
   [SerializeField] protected float maxBubbleHeight;
   [SerializeField] protected float Drag;
   [SerializeField] protected float Damp;
    [SerializeField] protected Rigidbody rb;
    public Vector2 bubbleDir;
    private Vector2 startPos;
    
   

    enum FloatState
    {
        onStart,
        onDrag,
        onChange,
    }
     void Start()=> startPos = transform.position;
    public void FixedUpdate(){FloatingMechanics();}

//Bubble Mechanics
public virtual void FloatingMechanics()
{

    rb.velocity =new Vector2(bubbleDir.x*floatingForce*Time.deltaTime*6,bubbleDir.y*floatingForce*Time.deltaTime*6) ;
    
    //
   // rb.AddForce(Vector2.down*Drag*maxBubbleHeight,ForceMode.Impulse);
    if(floatingForce>0)
    {
        floatingForce -=Damp*maxBubbleHeight*Time.deltaTime; 
    }
    else{floatingForce = 0.001f;}
    
 
  
}
public void DragForce()
{
     rb.AddForce(Vector2.down*Drag*maxBubbleHeight,ForceMode.Impulse);
}
public virtual void bubbleDirection(Vector3 hitObject , float force)
{
    this.floatingForce = force;
    Vector3 dir = transform.position-hitObject;
    dir.z =0;
    bubbleDir = dir;
   
    
}
public virtual void  ExplosiveMechanics()
{

}
 void OnTriggerEnter(Collider other)
 {
     IIntereactable Onhit = other.gameObject.GetComponent<IIntereactable>();
     if(Onhit!=null)
     {
        Onhit.OnHit();
        bubbleDirection(other.gameObject.transform.position,20);

     }
 }

}
