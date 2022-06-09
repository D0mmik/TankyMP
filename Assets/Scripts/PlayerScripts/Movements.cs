using AI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace PlayerScripts
{
    public class Movements : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject ui;
        [SerializeField] private Camera cam;
        [SerializeField] private AudioListener lis;

        private bool _randomizer;
        private int _randomMovement;
        private bool _instagib;

        private Belt _belt;
        private Hover _hover;
        private Fly _fly;

        public bool BeltActive;
        public bool HoverActive;
        public bool FlyActive;

        private SpawnAI _spawnAI;
        private GameObject _aiManager;
        public TMP_Text AIText;
        public GameObject AIButtons;

        void Start()
        {
            _randomizer = GameModes.S_Randomizer;
            _instagib = GameModes.S_Instagib;
        
            if(!photonView.IsMine)
            {
                cam.enabled = false;
                lis.enabled = false;
                ui.SetActive(false); 
            }             
           
        
    
            _belt = GetComponent<Belt>();
            _hover = GetComponent<Hover>();
            _fly = GetComponent<Fly>();
            TurnOffMovements();
            if(!_randomizer && !_instagib)
                BeltActive = true;
  
            if(_randomizer && !_instagib)
            {
                _randomMovement = Random.Range(1, 4);
                switch (_randomMovement)
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
            if(_instagib)
                HoverActive = true;

            _aiManager = GameObject.Find("AIManager");
            _spawnAI = _aiManager.GetComponent<SpawnAI>();
            AIText.text = _spawnAI.AICount.ToString();
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
            _belt.enabled = false;
            _hover.enabled = false;
            _fly.enabled = false;
        }

        public void EnableBelt()
        {
            TurnOffMovements();
            _belt.enabled = true;
            _belt.SetRb();
        }
        public void EnableHover()
        {
            TurnOffMovements();
            _hover.enabled = true;
            _hover.SetRb();
        }
        public void EnableFly()
        {
            TurnOffMovements();
            _fly.enabled = true;
            _fly.SetRb();
        }
        public void AiPlus()
        {
            if(_spawnAI.AICount < 10)
                _spawnAI.AICount++;

            AIText.text = _spawnAI.AICount.ToString();
            _spawnAI.Spawn();
        }
        public void AiMinus()
        {
            if(_spawnAI.AICount > 0)
                _spawnAI.AICount--;

            AIText.text = _spawnAI.AICount.ToString();
        }
        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            AIButtons.SetActive(PhotonNetwork.IsMasterClient); //plus a mínus
        }
    }
}
