using Photon.Pun;
using UnityEngine;

namespace PlayerScripts
{
    public class Billboard : MonoBehaviourPun
    {
        Transform camTransform;
        void Update()
        {
            if(Camera.main.transform == null)
                return;
    
            camTransform = Camera.main.transform;
        }
        private void LateUpdate()
        {
            if(transform != null && camTransform != null)
            {
                var rotation = camTransform.rotation;
                transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
            }
        }
    }
}
