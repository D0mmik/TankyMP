using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;

public class Load : MonoBehaviourPunCallbacks
{
   
   public GameObject[] armor;
   public Material[] color;
   public GameObject[] weapons;
   public GameObject tank;
   public int CurrentArmor;
   public int CurrentColor;
   public int CurrentWeapon;
   private int randomArmor;
   private int randomColor;
   private int randomWeapon;
   public ChangerIG ChangerIG;
   private bool randomizer;
   private bool instagib;



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
        if(photonView.IsMine && randomizer == false)
        {
            if(instagib == false)
            {
                LoadAll(PlayerPrefs.GetInt("armor"),PlayerPrefs.GetInt("color"),PlayerPrefs.GetInt("weapon"));
            }
            else if(instagib == true)
            {
                LoadAll(PlayerPrefs.GetInt("armor"),PlayerPrefs.GetInt("color"),0);
            }
        }
        if(photonView.IsMine && randomizer == true)
        {
            RandomSpawn();
        }
        //UpdateConfig();
        
   }

   public void RandomSpawn()
   {
       if(photonView.IsMine)
        {
            randomArmor = Random.Range(0,armor.Length);
            randomColor = Random.Range(0,color.Length);
            if(instagib == false)
            {
                randomWeapon = Random.Range(0, weapons.Length);
            }
            else if(instagib == true)
            {
                randomWeapon = 0;
            }
            PlayerPrefs.SetInt("armor", randomArmor); 
            PlayerPrefs.SetInt("color", randomColor);
            PlayerPrefs.SetInt("weapon", randomWeapon);
            LoadAll(randomArmor,randomColor,randomWeapon);
        }
        StartCoroutine(WaitForUpdate());
        
   }
   IEnumerator WaitForUpdate()
   {
        yield return new WaitForSeconds(0.1f);
        UpdateConfig();
   }

   public void UpdateConfig()
   {
        if(photonView.IsMine &&  randomizer == false)
        {
            LoadAll(PlayerPrefs.GetInt("armor"),PlayerPrefs.GetInt("color"),PlayerPrefs.GetInt("weapon"));
        }
        if(photonView.IsMine &&  randomizer == true)
        {
            LoadAll(randomArmor,randomColor,randomWeapon);
        }
   }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateConfig();
    }
    
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
       if(!changedProps.ContainsKey("armor") || !changedProps.ContainsKey("color") || !changedProps.ContainsKey("weapon"))   
        return;
        
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
        CurrentArmor = armorNumber;
        


        tank.GetComponent<MeshRenderer>().material = color[colorNumber];
        CurrentColor = colorNumber;

        foreach( var item in weapons)
        {
            item.SetActive(false);
        }
        weapons[weaponNumber].SetActive(true);
        CurrentWeapon = weaponNumber;

        if(photonView.IsMine)
        {
            Hashtable table = new Hashtable();
            table.Add("armor",CurrentArmor);
            table.Add("color",CurrentColor);
            table.Add("weapon",CurrentWeapon);
            if(PhotonNetwork.InRoom)
            {
                PhotonNetwork.LocalPlayer.SetCustomProperties(table);
            }
        }       
   }
    
   
}
