using Photon.Pun;
using PhotonMP;
using UnityEngine;

namespace ShootingScripts
{
    public class Scope : MonoBehaviourPun
    {
        public Camera MainCam;
        public GameObject ShootPoint;
        public GameObject CameraPoint;
        public GameObject CrossHair;
        float mouseWheel;
        public float ScrollSensitivity;
        public float TargetZoom = 60;
    
        float mouseY;
        public float Speed = 50;
        public GameObject WeaponHolder;
        public float VerticalWh;
        public float VMax = 8;
        public float VMin = -10;
        public float FOVMax = 75;
        public float FOVMin = 25;

        public static bool SScoped = false;
        void Start()
        {
            ScrollSensitivity = PlayerPrefs.GetFloat("ScrollSens", 30) * 100;
            if(photonView.IsMine)
                SScoped = false;
        }
    
        void Update()
        {

            if(!photonView.IsMine)
                return;
        
            if(Input.GetMouseButtonDown(1) && !SScoped && !PlayerLeave.Paused)
            {
                Transform transform1 = transform;
                transform1.position = ShootPoint.transform.position;
                transform1.rotation = ShootPoint.transform.rotation;
                SScoped = true;
                CrossHair.SetActive(true);
            }
            else if(Input.GetMouseButtonDown(1) && SScoped)
            {
                Transform transform1 = transform;
                transform1.position = CameraPoint.transform.position;
                transform1.rotation = CameraPoint.transform.rotation;
                SScoped = false;
                CrossHair.SetActive(false);
            }
        
        
            switch (SScoped)
            {
                case true:
                    mouseWheel = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity * Time.deltaTime * 50;
                    TargetZoom = TargetZoom -= mouseWheel;   

                    mouseY = Input.GetAxis("Mouse Y") * Speed * Time.deltaTime;
                    VerticalWh = VerticalWh -= mouseY;
                    break;
                case false:
                    TargetZoom = 60;
                    VerticalWh = 0;
                    break;
            }

            MainCam.fieldOfView = Mathf.MoveTowards(MainCam.fieldOfView, TargetZoom, 60 * Time.deltaTime);

            if(MainCam.fieldOfView >= FOVMax)
            { 
                MainCam.fieldOfView = FOVMax;
                TargetZoom = FOVMax;
            }
            if(MainCam.fieldOfView <= FOVMin)
            {
                MainCam.fieldOfView = FOVMin;
                TargetZoom = FOVMin;
            }

            if(VerticalWh >= VMax)
                VerticalWh = VMax;

            if(VerticalWh <= VMin)
                VerticalWh = VMin;
            
            WeaponHolder.transform.rotation = Quaternion.Euler(VerticalWh,WeaponHolder.transform.eulerAngles.y,0);
        
        }
    }
}
