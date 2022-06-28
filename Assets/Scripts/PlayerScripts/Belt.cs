using Photon.Pun;
using PhotonMP;
using UnityEngine;

namespace PlayerScripts
{
    public class Belt : MonoBehaviourPun
    {
        Rigidbody rb;
        [SerializeField] private float Speed = 10; 
        float horizontal;
        float vertical;
        Vector3 moveDirection;
        bool isGrounded;
        float groundDrag = 6f;
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
            Speed = upgradeSpeed;
        }
        void Update()
        {
            if(!photonView.IsMine)
                return;
            
            if(PlayerLeave.Paused)
                return;

            isGrounded = Physics.Raycast(transform.position, Vector3.down,0.01f);

            rb.drag = isGrounded? groundDrag : 6;

            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxis("Horizontal");
    
            moveDirection = transform.forward * vertical;
            transform.Rotate(horizontal * 100 * Time.deltaTime * Vector3.up);
        
        }

        void FixedUpdate()
        {
            if(!photonView.IsMine)
                return;

            var groundedSpeed = moveDirection.normalized * Speed;
            var airSpeed = groundedSpeed * AirMovement;
        
            rb.AddForce(isGrounded ? groundedSpeed : airSpeed, ForceMode.Acceleration);
        }
    }
}