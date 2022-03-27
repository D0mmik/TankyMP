using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviourPun
{
    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    public bool isGrounded;
    private Vector3 moveDirection;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Camera cam;
    [SerializeField] private AudioListener lis;
    [SerializeField] private float groundDrag = 6f;
    [SerializeField] private float airDrag = 2f;

    void Start()
    {   
        rb = GetComponent<Rigidbody>();
        if(photonView.IsMine == false)
        {
            cam.enabled = false;
            lis.enabled = false;

        }
    }
    void Update()
    {
        if(photonView.IsMine)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            moveDirection = transform.forward * vertical;
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 2f); 
            //Debug.DrawRay(transform.position, Vector3.down,Color.cyan,2+ 0.1f);

            if(isGrounded)
            {
                rb.drag = groundDrag;
            }
            else
            {
                rb.drag = airDrag;
            }
        }
    }
    void FixedUpdate()
    {
        rb.AddForce(moveDirection.normalized * speed);
        rb.AddTorque(new Vector3(0,horizontal * rotationSpeed, 0),ForceMode.Acceleration);
    }
}
