using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Fly : MonoBehaviourPun
{
    private Rigidbody rb;
    [SerializeField] private float speed = 20;
    [SerializeField] private float flySpeed = 5;
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
        if(photonView.IsMine == false)
        {
            rb.useGravity = false;
        }
    }
    public void ChangeSpeed(float upgradeSpeed)
    {
        speed = upgradeSpeed;
    }
    public void ChangeFlySpeed(float upgradeFlySpeed)
    {
        flySpeed = upgradeFlySpeed;
    }
    void Update()
    {  
        if(photonView.IsMine && PlayerLeave.paused == false)
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
        if(Input.GetKey(KeyCode.Space) && photonView.IsMine)
        {
            rb.AddForce(transform.up * flySpeed, ForceMode.Acceleration);
        }


        if(Input.GetKey(KeyCode.LeftShift) && photonView.IsMine)
        {
            rb.AddForce(-transform.up * flySpeed, ForceMode.Acceleration);
        }
        
    }
}
