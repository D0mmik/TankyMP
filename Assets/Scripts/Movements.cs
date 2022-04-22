using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class Movements : MonoBehaviourPun
{
    [SerializeField] private GameObject ui;
    [SerializeField] private Camera cam;
    [SerializeField] private AudioListener lis;

    private Belt belt;
    private Hover hover;
    private Fly fly;

    public bool beltActive;
    public bool hoverActive;
    public bool flyActive;

    void Start()
    {
        if(photonView.IsMine == false)
        {
            cam.enabled = false;
            lis.enabled = false;
            Destroy(ui);
        }
        if(photonView.IsMine)
        {
            belt = GetComponent<Belt>();
            hover = GetComponent<Hover>();
            fly = GetComponent<Fly>();
            TurnOffMovements();
            belt.enabled = true;
        }
    }
    void Update()
    {
        if(beltActive == true)
        {
            EnableBelt();
            Debug.Log("belt");
            beltActive = false;
        }
        if(hoverActive == true)
        {
            EnableHover();
            Debug.Log("hover");
            hoverActive = false;
        }
        if(flyActive == true)
        {
            EnableFly();
            Debug.Log("fly");
            flyActive = false;
        }
    }

    public void TurnOffMovements()
    {
        belt.enabled = false;
        hover.enabled = false;
        fly.enabled = false;
    }

    public void EnableBelt()
    {
        TurnOffMovements();
        belt.enabled = true;
        belt.SetRb();
    }
    public void EnableHover()
    {
        TurnOffMovements();
        hover.enabled = true;
        hover.SetRb();
    }
    public void EnableFly()
    {
        TurnOffMovements();
        fly.enabled = true;
        fly.SetRb();
    }
}
