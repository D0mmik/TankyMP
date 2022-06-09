using Photon.Pun;
using UnityEngine;

namespace PlayerScripts
{
    public class Billboard : MonoBehaviourPun
    {
        private Transform _camTransform;
    
        void Update()
        {
            if(Camera.main.transform == null)
                return;
    
            _camTransform = Camera.main.transform;
        }
        private void LateUpdate()
        {
            if(transform != null && _camTransform != null)
                transform.LookAt(transform.position + _camTransform.rotation * Vector3.forward, _camTransform.rotation * Vector3.up);
        }
    }
}
