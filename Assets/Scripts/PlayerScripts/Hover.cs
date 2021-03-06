using Photon.Pun;
using PhotonMP;
using UnityEngine;

namespace PlayerScripts
{
    public class Hover : MonoBehaviourPun
    {
        private Rigidbody rb;
        public float Speed;
        public Transform[] ForcePoints = new Transform[4];
        private RaycastHit[] hits = new RaycastHit[4];
        private float vertical;
        private float horizontal;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        public void SetRb()
        {
            rb.mass = 100;
            rb.drag = 2;
            rb.angularDrag = 2.1f;
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
        
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            transform.Rotate(100 * horizontal * Time.deltaTime * Vector3.up);
        
        }
        void FixedUpdate()
        {
            for(int i = 0; i < 4; i++)
                ApplyForce(ForcePoints[i], hits[i]);
            
            rb.AddForce(vertical * Speed* transform.forward);
        }
        void ApplyForce(Transform forcePoint, RaycastHit hit)
        {
            if(!photonView.IsMine)
                return;
            if (!Physics.Raycast(forcePoint.position, -forcePoint.up, out hit)) 
                return;
        
            float force = 0;
            force = Mathf.Abs(1 /(hit.point.y - transform.position.y));
            rb.AddForceAtPosition(force * 2.5f * transform.up, forcePoint.position, ForceMode.Acceleration);

        }
    }
}
