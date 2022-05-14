using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Billboard : MonoBehaviourPun
{
    private Transform camTransform;
    
    void Update()
    {
        if(Camera.main.transform == null)
            return;
    
        camTransform = Camera.main.transform;
    }
    private void LateUpdate()
    {
        if(transform != null && camTransform != null)
            transform.LookAt(transform.position + camTransform.rotation * Vector3.forward, camTransform.rotation * Vector3.up);
    }
}
