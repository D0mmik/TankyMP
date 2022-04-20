using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pohyb : MonoBehaviourPun
{
    private Rigidbody rb;
    [SerializeField] private float speed = 10;
    public float horizontal;
    public float vertical;
    public Vector3 moveDirection;
    public bool isGrounded;
    public float jumpForce = 10f;
    private float groundDrag = 6f;
    private float airDrag = 2f;
    public float airMovement = 0.4f;
    [SerializeField] private Camera cam;
    [SerializeField] private AudioListener lis;
    PlayerManager playerManager;
    public PauseMenu pauseMenu;
    public GameObject ui;

    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        if(photonView.IsMine == false)
        {
            cam.enabled = false;
            lis.enabled = false;
            Destroy(ui);
        }
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
            rb.AddForce(-transform.up * 100000, ForceMode.Acceleration);
        }
        
    }
}
