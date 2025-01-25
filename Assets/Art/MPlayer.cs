using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MPlayer : MonoBehaviour
{

    [SerializeField] private bool isGrounded;
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;

    private Rigidbody playerRb;

    private void Awake()
    {
        playerRb = this.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movementInput = new Vector3(horizontalInput, 0f, 0f);
        this.transform.position += movementInput * speed * Time.deltaTime;

        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * jumpForce);
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
