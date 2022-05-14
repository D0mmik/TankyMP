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
        if(!photonView.IsMine)
            rb.useGravity = false;
    }
    public void ChangeSpeed(float upgradeSpeed)
    {
        speed = upgradeSpeed;
    }
    void Update()
    {
        if(!photonView.IsMine)
            return;
            
        if(PlayerLeave.Paused)
            return;

        isGrounded = Physics.Raycast(transform.position, Vector3.down,0.01f);

        rb.drag = isGrounded? GroundDrag : 6;

        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxis("Horizontal");
    
        moveDirection = transform.forward * vertical;
        transform.Rotate(Vector3.up * 100 * horizontal * Time.deltaTime);
        
    }
    void FixedUpdate()
    {
        if(!photonView.IsMine)
            return;

        Vector3 groundedSpeed = moveDirection.normalized * speed;
        Vector3 airSpeed = groundedSpeed * AirMovement;
        
        rb.AddForce(isGrounded ? groundedSpeed : airSpeed, ForceMode.Acceleration);
        
        
    }
}
