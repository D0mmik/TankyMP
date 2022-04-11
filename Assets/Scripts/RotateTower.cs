using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotateTower : MonoBehaviourPun
{
    public Transform tower;
    private float mouseX;
    public float speed = 1000f;
    private float yRot;
    public float yRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        yRotation = 0;
        yRot = 0;
    }
    void Update()
    {
        if(photonView.IsMine == true)
        {
            mouseX = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
            yRot += mouseX;
            tower.localRotation = Quaternion.Euler(0,yRot,0);
        }
        
    }
}
