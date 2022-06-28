using Photon.Pun;
using PhotonMP;
using UnityEngine;

namespace PlayerScripts
{
    public class RotateTower : MonoBehaviourPun
    {
        public Transform Tower;
        float mouseX;
        public float Speed = 1000f; 
        float yRot;

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
        
            mouseX = Input.GetAxis("Mouse X") * Speed * 2 * Time.deltaTime; 
            yRot += mouseX;
            Tower.localRotation = Quaternion.Euler(0,yRot,0);
        }
    }
}
