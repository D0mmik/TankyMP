using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Scope : MonoBehaviourPun
{
    public Camera MainCam;
    public GameObject ShootPoint;
    public GameObject CameraPoint;
    public GameObject Crosshair;
    private float mouseWheel;
    public float ScrollSensitivity;
    public float TargetZoom = 60;
    
    private float mouseY;
    public float Speed = 50;
    public GameObject WeaponHolder;
    public float VerticalWH;
    public float VMax = 8;
    public float VMIn = -10;
    public float FOVMax = 75;
    public float FOVMin = 25;

    public static bool S_Scoped = false;
    void Start()
    {
        ScrollSensitivity = PlayerPrefs.GetFloat("ScrollSens", 30) * 100;
        if(photonView.IsMine)
            S_Scoped = false;
    }
    
    void Update()
    {

        if(!photonView.IsMine)
            return;
        
        if(Input.GetMouseButtonDown(1) && !S_Scoped && !PlayerLeave.Paused)
        {
            Transform transform1 = transform;
            transform1.position = ShootPoint.transform.position;
            transform1.rotation = ShootPoint.transform.rotation;
            S_Scoped = true;
            Crosshair.SetActive(true);
        }
        else if(Input.GetMouseButtonDown(1) && S_Scoped)
        {
            Transform transform1 = transform;
            transform1.position = CameraPoint.transform.position;
            transform1.rotation = CameraPoint.transform.rotation;
            S_Scoped = false;
            Crosshair.SetActive(false);
        }
        
        
        switch (S_Scoped)
        {
            case true:
                mouseWheel = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity * Time.deltaTime * 50;
                TargetZoom = TargetZoom -= mouseWheel;   

                mouseY = Input.GetAxis("Mouse Y") * Speed * Time.deltaTime;
                VerticalWH = VerticalWH -= mouseY;
                break;
            case false:
                TargetZoom = 60;
                VerticalWH = 0;
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

        if(VerticalWH >= VMax)
            VerticalWH = VMax;

        if(VerticalWH <= VMIn)
            VerticalWH = VMIn;
            
        WeaponHolder.transform.rotation = Quaternion.Euler(VerticalWH,WeaponHolder.transform.eulerAngles.y,0);
        
    }
}
