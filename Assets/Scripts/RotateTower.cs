using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotateTower : MonoBehaviourPun
{
    public Transform Tower;
    private float mouseX;
    public float Speed = 1000f;
    private float yRot;
    public float Yrotation;
    public PauseMenu PauseMenu;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Yrotation = 0;
        yRot = 0;
        Speed = PlayerPrefs.GetFloat("Sens", 40) * 100;
    }
    void Update()
    {
        if(photonView.IsMine == true && PlayerLeave.Paused == false)
        {
            mouseX = Input.GetAxis("Mouse X") * Speed * 2 * Time.deltaTime;
            yRot += mouseX;
            Tower.localRotation = Quaternion.Euler(0,yRot,0);
        }
        
    }
}
