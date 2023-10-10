using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 40f;
    public float jumpSpeed = 6f;
    public float rotationSpeed = 1000f;
    private Vector3 startingPosition;
    private bool isGrounded = true;
    private Quaternion startingRotation;
    

    Rigidbody rb;

    Collider coll;

    void Start()
    {
        

        rb = GetComponent<Rigidbody>();

        coll = GetComponent<Collider>();

        Cursor.lockState = CursorLockMode.Locked;

        startingPosition = transform.position;

        startingRotation = transform.rotation;

    }


    // Update is called once per frame
    void Update()
    {
        CursorLock();
        WalkHandler();
        JumpHandler();
        RotationHandler();
        resetPosition();
        
    }

    void resetPosition()
    {
        if (Input.GetKey(KeyCode.M))
        {
            rb.MovePosition(startingPosition);
            rb.MoveRotation(startingRotation);
        }
    }

    void CursorLock()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetKey(KeyCode.RightShift))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }


    void WalkHandler()
    {
        
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        float distance = walkSpeed * Time.deltaTime;
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hAxis * distance, 0f, vAxis * distance);

        movement = transform.TransformDirection(movement);

        rb.MovePosition(transform.position + movement);
    }

    void JumpHandler()
    {
       
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rb.velocity = new Vector3(0f, jumpSpeed, 0f);
        } 
        
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    isGrounded = true;
    //}

    void RotationHandler()
    {
        Vector3 newRotation = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        rb.transform.Rotate(newRotation * Time.deltaTime * rotationSpeed);
    }

    
}
