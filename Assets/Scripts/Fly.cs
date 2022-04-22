using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Fly : MonoBehaviourPun
{
    private Rigidbody rb;
    [SerializeField] private float speed = 20;
    public float horizontal;
    public float vertical;
    public Vector3 moveDirection;
    public bool isGrounded;
    private float groundDrag = 6f;
    private float airDrag = 2f;
    public float airMovement = 0.4f;
    

    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void SetRb()
    {
        rb.mass = 10;
        rb.drag = 0;
        rb.angularDrag = 10;
        rb.useGravity = false;
    }
    void Update()
    {  
        if(PlayerLeave.paused == false)
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down,0.01f);
            Debug.DrawRay(transform.position, Vector3.down,Color.black,0.01f);

            if(isGrounded)
            {
                rb.drag = groundDrag;
            } 
            else
            {
                rb.drag = airDrag;
            }

            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        
            moveDirection = transform.forward * vertical;
            transform.Rotate(Vector3.up * 100 * horizontal * Time.deltaTime);

            if(Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(transform.up * 5, ForceMode.Acceleration);
            }


            if(Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce(-transform.up * 5, ForceMode.Acceleration);
            }
        }    
        

    }
    void FixedUpdate()
    {
        if(isGrounded)
        {
            rb.AddForce(moveDirection.normalized * speed, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * speed * airMovement, ForceMode.Acceleration);
        }
        
    }
}
