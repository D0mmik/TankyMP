using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public GameObject cam;
    public GameObject shootPoint;
    public GameObject cameraPoint;
    public GameObject crossHair;

    private bool scoped = false;
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && scoped == false)
        {
            cam.transform.position = shootPoint.transform.position;
            cam.transform.rotation = shootPoint.transform.rotation;
            scoped = true;
            crossHair.SetActive(true);
        }
        else if(Input.GetMouseButtonDown(1) && scoped == true)
        {
            cam.transform.position = cameraPoint.transform.position;
            cam.transform.rotation = cameraPoint.transform.rotation;
            scoped = false;
            crossHair.SetActive(false);
        }
    }
}
