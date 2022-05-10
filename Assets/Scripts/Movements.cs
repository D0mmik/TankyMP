using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using TMPro;
using Photon.Realtime;

public class Movements : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject ui;
    [SerializeField] private Camera cam;
    [SerializeField] private AudioListener lis;

    private bool randomizer;
    private int randomMovement;
    private bool instagib;

    private Belt belt;
    private Hover hover;
    private Fly fly;

    public bool BeltActive;
    public bool HoverActive;
    public bool FlyActive;

    SpawnAI spawnAI;
    public GameObject AIManager;
    public TMP_Text AIText;
    public GameObject AIButtons;

    void Start()
    {
        if((bool)PhotonNetwork.CurrentRoom.CustomProperties["Randomizer"] == true)
        {
            randomizer = true;
        }
        if((bool)PhotonNetwork.CurrentRoom.CustomProperties["Instagib"] == true)
        {
            instagib = true;
        }
        if(photonView.IsMine == false)
        {
            cam.enabled = false;
            lis.enabled = false;
            ui.SetActive(false);
        }
    
        belt = GetComponent<Belt>();
        hover = GetComponent<Hover>();
        fly = GetComponent<Fly>();
        TurnOffMovements();
        if(randomizer == false && instagib == false)
        {
            BeltActive = true;
        }
  
        if(randomizer == true && instagib == false)
        {
            randomMovement = Random.Range(1, 4);
            if(randomMovement == 1){BeltActive = true;}
            if(randomMovement == 2){FlyActive = true;}
            if(randomMovement == 3){HoverActive = true;}
        }
        if(instagib == true)
        {
            HoverActive = true;
        }
        AIManager = GameObject.Find("AIManager");
        spawnAI = AIManager.GetComponent<SpawnAI>();
        AIText.text = spawnAI.AICount.ToString();
        AIButtons.SetActive(PhotonNetwork.IsMasterClient);

    }
    void Update()
    {
        if(photonView.IsMine)
        {
            cam.enabled = true;
            ui.SetActive(true);
        }

        if(BeltActive == true)
        {
            EnableBelt();
            BeltActive = false;
        }
        if(HoverActive == true)
        {
            EnableHover();
            HoverActive = false;
        }
        if(FlyActive == true)
        {
            EnableFly();
            FlyActive = false;
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
    public void AiPlus()
    {
        if(spawnAI.AICount < 10)
        {
            spawnAI.AICount++;
        }
        AIText.text = spawnAI.AICount.ToString();
        spawnAI.Spawn();
    }
    public void AiMinus()
    {
        if(spawnAI.AICount > 0)
        {
            spawnAI.AICount--;
        }
        AIText.text = spawnAI.AICount.ToString();
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        AIButtons.SetActive(PhotonNetwork.IsMasterClient); //plus a mínus
    }
}
