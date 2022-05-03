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

    private bool randomizer;
    private int randomMovement;

    private Belt belt;
    private Hover hover;
    private Fly fly;

    public bool beltActive;
    public bool hoverActive;
    public bool flyActive;

    SpawnAI spawnAI;
    public GameObject aiManager;

    void Start()
    {
        if((bool)PhotonNetwork.CurrentRoom.CustomProperties["Randomizer"] == true)
        {
            randomizer = true;
        }
        if(photonView.IsMine == false)
        {
            cam.enabled = false;
            lis.enabled = false;
            Destroy(ui);
        }
    
        belt = GetComponent<Belt>();
        hover = GetComponent<Hover>();
        fly = GetComponent<Fly>();
        TurnOffMovements();
        beltActive = true;
        
        if(randomizer == true)
        {
            randomMovement = Random.Range(1, 4);
            if(randomMovement == 1){beltActive = true;}
            if(randomMovement == 2){flyActive = true;}
            if(randomMovement == 3){hoverActive = true;}
        }
        aiManager = GameObject.Find("AIManager");
        spawnAI = aiManager.GetComponent<SpawnAI>();

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && photonView.IsMine)
        {
            spawnAI.Spawn();
        }


        if(beltActive == true)
        {
            EnableBelt();
            beltActive = false;
        }
        if(hoverActive == true)
        {
            EnableHover();
            hoverActive = false;
        }
        if(flyActive == true)
        {
            EnableFly();
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
