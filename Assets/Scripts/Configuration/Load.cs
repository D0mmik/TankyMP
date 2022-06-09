using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using PlayerScripts;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Configuration
{
    public class Load : MonoBehaviourPunCallbacks
    {
   
        public GameObject[] Armor;
        public Material[] Color;
        public GameObject[] Weapons;
        public GameObject Tank;
        public int CurrentArmor;
        public int CurrentColor;
        public int CurrentWeapon;
        private int _randomArmor;
        private int _randomColor;
        private int _randomWeapon;
        private bool _randomizer;
        private bool _instagib;
        private MeshRenderer _meshRenderer;


        void Start()
        {
            _meshRenderer = Tank.GetComponent<MeshRenderer>();
            _randomizer = GameModes.S_Randomizer;
            _instagib = GameModes.S_Instagib;
       
            if(!photonView.IsMine)
                return;

            if(!_randomizer)
            {
                if(!_instagib)
                    LoadAll(PlayerPrefs.GetInt("armor"),PlayerPrefs.GetInt("color"),PlayerPrefs.GetInt("weapon"));
                else if(_instagib)
                    LoadAll(PlayerPrefs.GetInt("armor"),PlayerPrefs.GetInt("color"),0);

            }
            if(_randomizer)
                RandomSpawn();       
        }
        private void RandomSpawn()
        {
            if(!photonView.IsMine)
                return;
        
            _randomArmor = Random.Range(0,Armor.Length);
            _randomColor = Random.Range(0,Color.Length);
            if(!_instagib)
                _randomWeapon = Random.Range(0, Weapons.Length);
            else if(_instagib) 
                _randomWeapon = 0;

            PlayerPrefs.SetInt("armor", _randomArmor); 
            PlayerPrefs.SetInt("color", _randomColor);
            PlayerPrefs.SetInt("weapon", _randomWeapon);
            LoadAll(_randomArmor,_randomColor,_randomWeapon);
               
            StartCoroutine(WaitForUpdate());
        
        }
        IEnumerator WaitForUpdate()
        {
            yield return new WaitForSeconds(0.1f);
            UpdateConfig();
        }

        public void UpdateConfig()
        {
            if(!photonView.IsMine)
                return;
       
            if(!_randomizer) 
                LoadAll(PlayerPrefs.GetInt("armor"),PlayerPrefs.GetInt("color"),PlayerPrefs.GetInt("weapon"));
            else if(_randomizer)
                LoadAll(_randomArmor,_randomColor,_randomWeapon);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            UpdateConfig();
        }
    
        public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            if(!changedProps.ContainsKey("armor") || !changedProps.ContainsKey("color") || !changedProps.ContainsKey("weapon"))   
                return;
        
            if(!photonView.IsMine && Equals(targetPlayer, photonView.Owner))
                LoadAll((int)changedProps["armor"],(int)changedProps["color"],(int)changedProps["weapon"]);
        }
    

        public void LoadAll(int armorNumber, int colorNumber, int weaponNumber)
        {
       
            foreach( var item in Armor)
                item.SetActive(false);

            Armor[armorNumber].SetActive(true);
            CurrentArmor = armorNumber;
        
            _meshRenderer = Tank.GetComponent<MeshRenderer>();
            _meshRenderer.material = Color[colorNumber];
            CurrentColor = colorNumber;

            foreach( var item in Weapons)
                item.SetActive(false);

            Weapons[weaponNumber].SetActive(true);
            CurrentWeapon = weaponNumber;
        
            if(!photonView.IsMine)
                return;
        
            Hashtable table = new Hashtable
            {
                { "armor", CurrentArmor },
                { "color", CurrentColor },
                { "weapon", CurrentWeapon }
            };
            if(!PhotonNetwork.InRoom)
                return;
    
            PhotonNetwork.LocalPlayer.SetCustomProperties(table);      
        } 
    }
}