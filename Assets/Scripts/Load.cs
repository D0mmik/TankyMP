using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Load : MonoBehaviourPunCallbacks
{
   public GameObject[] armor;
   public Material[] color;
   //public GameObject[] propulsion;
   public GameObject[] weapons;
   public GameObject tank;
   [SerializeField] private int currentArmor;
   [SerializeField] private int currentColor;
   [SerializeField] private int currentWeapon;
   void Start()
   {
       if(photonView.IsMine)
        {
            LoadAll(PlayerPrefs.GetInt("armor"),PlayerPrefs.GetInt("color"),PlayerPrefs.GetInt("weapon"));
        }
   }
   
    public void Update()
    {
        if(photonView.IsMine)
        {
            if(Input.GetKeyDown(KeyCode.L))
            {
                LoadAll(PlayerPrefs.GetInt("armor"),PlayerPrefs.GetInt("color"),PlayerPrefs.GetInt("weapon"));
            }
        }
        
    }
    
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
       if(!photonView.IsMine && targetPlayer == photonView.Owner)
       {
           LoadAll((int)changedProps["armor"],(int)changedProps["color"],(int)changedProps["weapon"]);
       } 
    }
    

   public void LoadAll(int armorNumber, int colorNumber, int weaponNumber)
   {
       
        foreach( var item in armor)
        {
            item.SetActive(false);
        }
        armor[armorNumber].SetActive(true);
        //tank = GameObject.Find("Body");
        currentArmor = armorNumber; 


        tank.GetComponent<MeshRenderer>().material = color[colorNumber];
        currentColor = colorNumber;

        foreach( var item in weapons)
        {
            item.SetActive(false);
        }
        weapons[weaponNumber].SetActive(true);
        currentWeapon = weaponNumber;

        if(photonView.IsMine)
        {
            Hashtable table = new Hashtable();
            table.Add("armor",currentArmor);
            table.Add("color",currentColor);
            table.Add("weapon",currentWeapon);
            PhotonNetwork.LocalPlayer.SetCustomProperties(table);
        }       
   }
   
}
