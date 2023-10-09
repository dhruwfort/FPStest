using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 40f;
    public float jumpSpeed = 6f;
    public float rotationSpeed = 100f;

    Rigidbody rb;

    Collider coll;

    bool pressedJump = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        coll = GetComponent<Collider>();
    }


    // Update is called once per frame
    void Update()
    {
        CursorLock();
        WalkHandler();
        JumpHandler();
        RotationHandler();
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
        float jAxis = Input.GetAxis("Jump");

        bool isGrounded = CheckGrounded();

        if (jAxis > 0f)
        {
            if (!pressedJump && isGrounded)
            {
                pressedJump = true;

                Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);

                rb.velocity = rb.velocity + jumpVector;
            }

        }
        else
        {
            pressedJump = false;
        }
    }

    void RotationHandler()
    {
        Vector3 newRotation = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        rb.transform.Rotate(newRotation * Time.deltaTime * rotationSpeed);
    }

    bool CheckGrounded()
    {
        float sizeX = coll.bounds.size.x;
        float sizeZ = coll.bounds.size.z;
        float sizeY = coll.bounds.size.y;


        Vector3 corner1 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
        Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
        Vector3 corner3 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);
        Vector3 corner4 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);

        bool grounded1 = Physics.Raycast(corner1, new Vector3(0, -1, 0), 0.01f);
        bool grounded2 = Physics.Raycast(corner2, new Vector3(0, -1, 0), 0.01f);
        bool grounded3 = Physics.Raycast(corner3, new Vector3(0, -1, 0), 0.01f);
        bool grounded4 = Physics.Raycast(corner4, new Vector3(0, -1, 0), 0.01f);

        return (grounded1 || grounded2 || grounded3 || grounded4);

    }
}
