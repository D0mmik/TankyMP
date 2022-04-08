using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Billboard : MonoBehaviourPun
{
    private Transform camTransform;
    
    void Start()
    {
        FindBillboard();
        Debug.Log("ok");
    }
    void FindBillboard()
    {
        if(Camera.main.transform != null)
        {
            camTransform = Camera.main.transform;
        }
    }
    private void LateUpdate()
    {
        if(transform != null && camTransform != null)
        {
            transform.LookAt(transform.position + camTransform.rotation * Vector3.forward, camTransform.rotation * Vector3.up);
        }
    }
}
