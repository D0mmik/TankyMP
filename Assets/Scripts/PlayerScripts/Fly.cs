using Photon.Pun;
using PhotonMP;
using UnityEngine;

namespace PlayerScripts
{
    public class Fly : MonoBehaviourPun
    {
        private Rigidbody rb;
        [SerializeField] private float Speed = 20;
        [SerializeField] private float FlySpeed = 5;
        float horizontal;
        float vertical;
        Vector3 moveDirection;
        bool isGrounded;
        [SerializeField] private float GroundDrag = 6f;
        public float AirDrag = 2f;
        public float AirMovement = 0.4f;
    
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
            if(!photonView.IsMine)      
                rb.useGravity = false;
        
        }
        public void ChangeSpeed(float upgradeSpeed)
        {
            Speed = upgradeSpeed;
        }
        public void ChangeFlySpeed(float upgradeFlySpeed)
        {
            FlySpeed = upgradeFlySpeed;
        }
        void Update()
        {   
            if(!photonView.IsMine)
                return;
            if(PlayerLeave.Paused)
                return;
        
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        
            isGrounded = Physics.Raycast(transform.position, Vector3.down,0.01f);

            rb.drag = isGrounded ? GroundDrag : AirDrag;

            moveDirection = transform.forward * vertical;
        
            transform.Rotate(100 * horizontal * Time.deltaTime * Vector3.up);    
        
        }
        void FixedUpdate()
        {
            if(!photonView.IsMine)
                return;

            var groundSpeed = moveDirection.normalized * Speed;
            var airSpeed = groundSpeed * AirMovement;

            rb.AddForce(isGrounded ? groundSpeed : airSpeed, ForceMode.Acceleration);

            if(Input.GetKey(KeyCode.Space))
                rb.AddForce(transform.up * FlySpeed, ForceMode.Acceleration);

            if(Input.GetKey(KeyCode.LeftShift))
                rb.AddForce(-transform.up * FlySpeed, ForceMode.Acceleration);
        
        }
    }
}
