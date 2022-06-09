using Photon.Pun;
using PhotonMP;
using UnityEngine;

namespace PlayerScripts
{
    public class Hover : MonoBehaviourPun
    {
        private Rigidbody _rb;
        public float Speed;
        public Transform[] ForcePoints = new Transform[4];
        private RaycastHit[] _hits = new RaycastHit[4];
        private float _vertical;
        private float _horizontal;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        public void SetRb()
        {
            _rb.mass = 100;
            _rb.drag = 2;
            _rb.angularDrag = 2.1f;
            _rb.useGravity = true;
            if(!photonView.IsMine)
                _rb.useGravity = false;    
        
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
        
            _vertical = Input.GetAxis("Vertical");
            _horizontal = Input.GetAxis("Horizontal");
            transform.Rotate(100 * _horizontal * Time.deltaTime * Vector3.up);
        
        }
        void FixedUpdate()
        {
            for(int i = 0; i < 4; i++)
                ApplyForce(ForcePoints[i], _hits[i]);
            
            _rb.AddForce(_vertical * Speed* transform.forward);
        }
        void ApplyForce(Transform forcePoint, RaycastHit hit)
        {
            if(!photonView.IsMine)
                return;
            if (!Physics.Raycast(forcePoint.position, -forcePoint.up, out hit)) 
                return;
        
            float force = 0;
            force = Mathf.Abs(1 /(hit.point.y - transform.position.y));
            _rb.AddForceAtPosition(force * 2.5f * transform.up, forcePoint.position, ForceMode.Acceleration);

        }
    }
}
