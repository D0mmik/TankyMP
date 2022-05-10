using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Belt : MonoBehaviourPun
{
    private Rigidbody rb;
    [SerializeField] private float speed = 10;
    private float horizontal;
    private float vertical;
    private Vector3 moveDirection;
    private bool isGrounded;
    public float JumpForce = 10f;
    private float GroundDrag = 6f;
    public float AirMovement = 0.4f;
    

    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void SetRb()
    {
        rb.mass = 10;
        rb.drag = 6;
        rb.angularDrag = 10;
        rb.useGravity = true;
        if(photonView.IsMine == false)
        {
            rb.useGravity = false;
        }
    }
    public void ChangeSpeed(float upgradeSpeed)
    {
        speed = upgradeSpeed;
    }
    void Update()
    {  
        if(photonView.IsMine && PlayerLeave.Paused == false)
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down,0.01f);
            //Debug.DrawRay(transform.position, Vector3.down,Color.black,0.01f);

            rb.drag = isGrounded? GroundDrag : 6;

            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        
            moveDirection = transform.forward * vertical;
            transform.Rotate(Vector3.up * 100 * horizontal * Time.deltaTime);
        }    
        

    }
    void FixedUpdate()
    {
        if(photonView.IsMine)
        {
            if(isGrounded)
            {
                rb.AddForce(moveDirection.normalized * speed, ForceMode.Acceleration);
            }
            else
            {
                rb.AddForce(moveDirection.normalized * speed * AirMovement, ForceMode.Acceleration);
            }
        }
        
    }
}
