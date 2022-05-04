using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Scope : MonoBehaviourPun
{
    public GameObject cam;
    public Camera mainCam;
    public GameObject shootPoint;
    public GameObject cameraPoint;
    public GameObject crossHair;
    private float mouseWheel;
    public float scrollSensitivity;
    public float targetZoom = 60;
    
    private float mouseY;
    public float speed;
    public GameObject weaponHolder;
    public float VerticalWH;

    public static bool scoped = false;
    void Start()
    {
        scrollSensitivity = PlayerPrefs.GetFloat("ScrollSens", 30) * 100;
    }
    
    void Update()
    {

        if(photonView.IsMine)
        {
            if(Input.GetMouseButtonDown(1) && scoped == false && PlayerLeave.paused == false)
            {
                cam.transform.position = shootPoint.transform.position;
                cam.transform.rotation = shootPoint.transform.rotation;
                scoped = true;
                crossHair.SetActive(true);
                Debug.Log("nene");
            }
            else if(Input.GetMouseButtonDown(1) && scoped == true)
            {
                cam.transform.position = cameraPoint.transform.position;
                cam.transform.rotation = cameraPoint.transform.rotation;
                scoped = false;
                crossHair.SetActive(false);
            }
            
           
            if(scoped == true)
            {
                mouseWheel = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity * Time.deltaTime * 50;
                targetZoom = targetZoom -= mouseWheel;   

                mouseY = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;
                VerticalWH = VerticalWH -= mouseY;
 
            }
            if(scoped == false)
            {
                targetZoom = 60;
                VerticalWH = 0;
            }
            mainCam.fieldOfView = Mathf.MoveTowards(mainCam.fieldOfView, targetZoom, 60 * Time.deltaTime);

           if(mainCam.fieldOfView >= 75)
           { 
              mainCam.fieldOfView = 75;
              targetZoom = 75;
           }
           if(mainCam.fieldOfView <= 25)
           {
              mainCam.fieldOfView = 25;
              targetZoom = 25;
           }

           if(VerticalWH >= 8)
           {
               VerticalWH = 8;
           }
           if(VerticalWH <= -10)
           {
               VerticalWH = -10;
           }
           weaponHolder.transform.rotation = Quaternion.Euler(VerticalWH,weaponHolder.transform.eulerAngles.y,0);
        }
    }
}
