using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Billboard : MonoBehaviourPun
{
    private Transform camTransform;
    
    void Start()
    {
        if(Camera.main.transform != null)
        {
            camTransform = Camera.main.transform;
        }
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + camTransform.rotation * Vector3.forward, camTransform.rotation * Vector3.up);
    }
}
