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

    public bool beltActive;
    public bool hoverActive;
    public bool flyActive;

    SpawnAI spawnAI;
    public GameObject aiManager;
    public TMP_Text aiText;
    public GameObject aiButtons;

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

        if(instagib == true)
        {
            hoverActive = true;
        }
        else
        {
            beltActive = true;
        }
  
        if(randomizer == true)
        {
            randomMovement = Random.Range(1, 4);
            if(randomMovement == 1){beltActive = true;}
            if(randomMovement == 2){flyActive = true;}
            if(randomMovement == 3){hoverActive = true;}
        }
        aiManager = GameObject.Find("AIManager");
        spawnAI = aiManager.GetComponent<SpawnAI>();
        aiText.text = spawnAI.aiCount.ToString();
        aiButtons.SetActive(PhotonNetwork.IsMasterClient);

    }
    void Update()
    {
        if(photonView.Owner.IsLocal)
        {
            cam.enabled = true;
            ui.SetActive(true);
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
    public void AiPlus()
    {
        if(spawnAI.aiCount < 10)
        {
            spawnAI.aiCount++;
        }
        aiText.text = spawnAI.aiCount.ToString();
        spawnAI.Spawn();
    }
    public void AiMinus()
    {
        if(spawnAI.aiCount > 0)
        {
            spawnAI.aiCount--;
        }
        aiText.text = spawnAI.aiCount.ToString();
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        aiButtons.SetActive(PhotonNetwork.IsMasterClient);
    }
}
