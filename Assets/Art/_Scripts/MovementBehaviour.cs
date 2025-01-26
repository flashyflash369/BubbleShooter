using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float turnSpeed;

    private Rigidbody playerRb;
    [SerializeField] private Transform graphic;
    //Bools
    [SerializeField] private bool isGrounded;

    //Inputs
    private float horizontalInput;
    private Vector3 movementDirection;

    private void Awake()
    {
        playerRb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movementInput = new Vector3(horizontalInput, 0f, 0f);
        movementDirection = movementInput.normalized;
    }
    private void Update()
    {
      
        this.transform.position += movementDirection * moveSpeed * Time.deltaTime;

        //Change forward direction of graphic
        //this.transform.forward = Vector3.Lerp(this.transform.forward, movementDirection, turnSpeed * Time.deltaTime);

        if(horizontalInput > 0.1)
        {
            graphic.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        else if(horizontalInput < 0f)
        {
            graphic.rotation = Quaternion.Euler(0f, -90f, 0f);
        }
        else
        {
            graphic.rotation = Quaternion.identity;
        }


        graphic.forward = Vector3.Lerp(graphic.forward, movementDirection, turnSpeed * Time.deltaTime);
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision col)
    {
        isGrounded = false;
    }


}
