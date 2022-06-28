using AI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace PlayerScripts
{
    public class Movements : MonoBehaviourPunCallbacks
    {
        [SerializeField] GameObject UI;
        [SerializeField] Camera Cam;
        [SerializeField] AudioListener Lis;

        bool randomizer;
        int randomMovement;
        bool instagib;

        Belt belt;
        Hover hover;
        Fly fly;

        public bool BeltActive;
        public bool HoverActive;
        public bool FlyActive;

        SpawnAI spawnAI;
        GameObject aiManager;
        public TMP_Text AIText;
        public GameObject AIButtons;

        void Start()
        {
            randomizer = GameModes.SRandomizer;
            instagib = GameModes.SInstagib;
        
            if(!photonView.IsMine)
            {
                Cam.enabled = false;
                Lis.enabled = false;
                UI.SetActive(false); 
            }             
           
        
    
            belt = GetComponent<Belt>();
            hover = GetComponent<Hover>();
            fly = GetComponent<Fly>();
            TurnOffMovements();
            if(!randomizer && !instagib)
                BeltActive = true;
  
            if(randomizer && !instagib)
            {
                randomMovement = Random.Range(1, 4);
                switch (randomMovement)
                {
                    case 1:
                        BeltActive = true;
                        break;
                    case 2:
                        FlyActive = true;
                        break;
                    case 3:
                        HoverActive = true;
                        break;
                }
            }
            if(instagib)
                HoverActive = true;

            aiManager = GameObject.Find("AIManager");
            spawnAI = aiManager.GetComponent<SpawnAI>();
            AIText.text = spawnAI.AICount.ToString();
            AIButtons.SetActive(PhotonNetwork.IsMasterClient);

        }
        void Update()
        {
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

        private void TurnOffMovements()
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
                spawnAI.AICount++;

            AIText.text = spawnAI.AICount.ToString();
            spawnAI.Spawn();
        }
        public void AiMinus()
        {
            if(spawnAI.AICount > 0)
                spawnAI.AICount--;

            AIText.text = spawnAI.AICount.ToString();
        }
        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            AIButtons.SetActive(PhotonNetwork.IsMasterClient); //plus a mínus
        }
    }
}
