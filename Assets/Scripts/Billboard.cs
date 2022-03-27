using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTransform;

    private void LateUpdate()
    {
        camTransform = Camera.main.transform;
        transform.LookAt(transform.position + camTransform.rotation * Vector3.forward, camTransform.rotation * Vector3.up);
    }
}
