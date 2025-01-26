using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timer = 5f;          // Lifetime of the bullet
    public float speed = 100f;       // Movement speed
    public float reflectSpeed;
    private float timeReset;         // Initial timer value
    private Rigidbody rb;          // Rigidbody reference
    private bool isDeactivated;      // Prevent duplicate deactivations
    Vector3 reflectedVector;
   
    public string[] paintString;

    void OnEnable()
    {
        // Reset timer and flag when bullet is enabled
       
        isDeactivated = false;
        reflectedVector = Vector3.zero;
       
    }

    void Start()
    {
        timeReset = timer;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Reduce timer and disable bullet when it runs out
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (!isDeactivated)
        {
            timer = timeReset;
            DisableObject();
             
        }
    }

    void FixedUpdate()
    {
        // Update bullet velocity
      //  rb.AddForce(-transform.right.normalized* speed * Time.fixedDeltaTime*6, ForceMode2D.Force);
       
    
    rb.velocity = -transform.right* speed * Time.fixedDeltaTime*6;
     // rb.velocity = new Vector3(  rb.velocity.x * speed * Time.fixedDeltaTime*6,rb.velocity.y,rb.velocity.z);
     
     
     
    }
    private void DisableObject()
    {
        if (isDeactivated) return; // Prevent multiple calls
        isDeactivated = true;

        // Return the bullet to the pool
        ObjectPool.instance.ReturnObject("Bullet", gameObject);
    }

    
    void OnTriggerEnter(Collider collider)
    {
//        GameObject gameObject =   ObjectPool.instance.GetObject(paintString[UnityEngine.Random.Range(0, paintString.Length)]);
       // if (gameObject != null){gameObject.transform.position = transform.position;}
       
        // Handle damage if target implements IDamagabl
        if (rb.velocity != Vector3.zero && collider.gameObject.tag == "Bounce")
        {
            // Calculate the direction of movement

            Vector2 movementDirection = transform.position.normalized;
            reflectedVector = Vector3.Reflect(movementDirection,Vector2.up);
            //Debug.DrawRay(transform.position, rb.velocity, Color.red);
          //  transform.position = reflectedVector;
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y,(Mathf.Rad2Deg*Mathf.Atan2(movementDirection.y,movementDirection.x))-180);
         
        }
        
        
        
        
        if (isDeactivated) return; // Ignore collisions if already deactivated
        // Deactivate the bullet
        if(collider.gameObject.tag == "Bounce" == false)
        {
           // DisableObject();
        }
       
       
    }
}
