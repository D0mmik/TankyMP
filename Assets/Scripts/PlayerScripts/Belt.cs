using Photon.Pun;
using PhotonMP;
using UnityEngine;

namespace PlayerScripts
{
    public class Belt : MonoBehaviourPun
    {
        private Rigidbody _rb;
        [SerializeField] private float speed = 10;
        private float _horizontal;
        private float _vertical;
        private Vector3 _moveDirection;
        private bool _isGrounded;
        private float _groundDrag = 6f;
        public float AirMovement = 0.4f;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        public void SetRb()
        {
            _rb.mass = 10;
            _rb.drag = 6;
            _rb.angularDrag = 10;
            _rb.useGravity = true;
            if(!photonView.IsMine)
                _rb.useGravity = false;
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

            _isGrounded = Physics.Raycast(transform.position, Vector3.down,0.01f);

            _rb.drag = _isGrounded? _groundDrag : 6;

            _vertical = Input.GetAxisRaw("Vertical");
            _horizontal = Input.GetAxis("Horizontal");
    
            _moveDirection = transform.forward * _vertical;
            transform.Rotate(_horizontal * 100 * Time.deltaTime * Vector3.up);
        
        }

        void FixedUpdate()
        {
            if(!photonView.IsMine)
                return;

            var groundedSpeed = _moveDirection.normalized * speed;
            var airSpeed = groundedSpeed * AirMovement;
        
            _rb.AddForce(_isGrounded ? groundedSpeed : airSpeed, ForceMode.Acceleration);
        }
    }
}