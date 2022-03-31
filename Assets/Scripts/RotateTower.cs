using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotateTower : MonoBehaviourPun
{
    public Transform tower;
    private float mouseX;
    private float speed = 100f;
    private float yRot;
    public float yRotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if(photonView.IsMine == true)
        {
            mouseX = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
            yRot += mouseX;
            yRotation = Mathf.MoveTowards(yRotation, yRot, 0.5f);

            tower.localRotation = Quaternion.Euler(0,yRotation,0);


        }
        
    }
}
