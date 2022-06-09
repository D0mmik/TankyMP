using Photon.Pun;
using PhotonMP;
using UnityEngine;

namespace PlayerScripts
{
    public class RotateTower : MonoBehaviourPun
    {
        public Transform Tower;
        private float _mouseX;
        public float Speed = 1000f;
        private float _yRot;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Speed = PlayerPrefs.GetFloat("Sens", 40) * 100;
        }
        void Update()
        {
            if(!photonView.IsMine)
                return;
            if(PlayerLeave.Paused)
                return;
        
            _mouseX = Input.GetAxis("Mouse X") * Speed * 2 * Time.deltaTime;
            _yRot += _mouseX;
            Tower.localRotation = Quaternion.Euler(0,_yRot,0);
        }
    }
}
