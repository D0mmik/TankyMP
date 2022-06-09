using Photon.Pun;
using PhotonMP;
using UnityEngine;

namespace PlayerScripts
{
    public class Fly : MonoBehaviourPun
    {
        private Rigidbody _rb;
        [SerializeField] private float speed = 20;
        [SerializeField] private float flySpeed = 5;
        private float _horizontal;
        private float _vertical;
        private Vector3 _moveDirection;
        private bool _isGrounded;
        [SerializeField] private float groundDrag = 6f;
        public float AirDrag = 2f;
        public float AirMovement = 0.4f;
    
        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        public void SetRb()
        {
            _rb.mass = 10;
            _rb.drag = 0;
            _rb.angularDrag = 10;
            _rb.useGravity = false;
            if(!photonView.IsMine)      
                _rb.useGravity = false;
        
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
            if(!photonView.IsMine)
                return;
            if(PlayerLeave.Paused)
                return;
        
            _vertical = Input.GetAxisRaw("Vertical");
            _horizontal = Input.GetAxis("Horizontal");
        
            _isGrounded = Physics.Raycast(transform.position, Vector3.down,0.01f);

            _rb.drag = _isGrounded ? groundDrag : AirDrag;

            _moveDirection = transform.forward * _vertical;
        
            transform.Rotate(100 * _horizontal * Time.deltaTime * Vector3.up);    
        
        }
        void FixedUpdate()
        {
            if(!photonView.IsMine)
                return;

            var groundSpeed = _moveDirection.normalized * speed;
            var airSpeed = groundSpeed * AirMovement;

            _rb.AddForce(_isGrounded ? groundSpeed : airSpeed, ForceMode.Acceleration);

            if(Input.GetKey(KeyCode.Space))
                _rb.AddForce(transform.up * flySpeed, ForceMode.Acceleration);

            if(Input.GetKey(KeyCode.LeftShift))
                _rb.AddForce(-transform.up * flySpeed, ForceMode.Acceleration);
        
        }
    }
}
