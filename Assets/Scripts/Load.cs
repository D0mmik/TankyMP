using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;

public class Load : MonoBehaviourPunCallbacks
{
   
   public GameObject[] Armor;
   public Material[] Color;
   public GameObject[] Weapons;
   public GameObject Tank;
   public int CurrentArmor;
   public int CurrentColor;
   public int CurrentWeapon;
   private int randomArmor;
   private int randomColor;
   private int randomWeapon;
   private bool randomizer;
   private bool instagib;
   private MeshRenderer meshRenderer;


   void Start()
   {
       meshRenderer = Tank.GetComponent<MeshRenderer>();
       randomizer = GameModes.S_Randomizer;
       instagib = GameModes.S_Instagib;
       
       if(!photonView.IsMine)
            return;

       if(!randomizer)
       {
            if(!instagib)
                LoadAll(PlayerPrefs.GetInt("armor"),PlayerPrefs.GetInt("color"),PlayerPrefs.GetInt("weapon"));
            else if(instagib)
                LoadAll(PlayerPrefs.GetInt("armor"),PlayerPrefs.GetInt("color"),0);

       }
       if(randomizer)
            RandomSpawn();       
   }
   private void RandomSpawn()
   {
       if(!photonView.IsMine)
            return;
        
       randomArmor = Random.Range(0,Armor.Length);
       randomColor = Random.Range(0,Color.Length);
       if(!instagib)
            randomWeapon = Random.Range(0, Weapons.Length);
       else if(instagib) 
           randomWeapon = 0;

       PlayerPrefs.SetInt("armor", randomArmor); 
       PlayerPrefs.SetInt("color", randomColor);
       PlayerPrefs.SetInt("weapon", randomWeapon);
       LoadAll(randomArmor,randomColor,randomWeapon);
               
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
       
       if(!randomizer) 
           LoadAll(PlayerPrefs.GetInt("armor"),PlayerPrefs.GetInt("color"),PlayerPrefs.GetInt("weapon"));
       else if(randomizer)
           LoadAll(randomArmor,randomColor,randomWeapon);
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
        
        meshRenderer = Tank.GetComponent<MeshRenderer>();
        meshRenderer.material = Color[colorNumber];
        CurrentColor = colorNumber;

        foreach( var item in Weapons)
            item.SetActive(false);

        Weapons[weaponNumber].SetActive(true);
        CurrentWeapon = weaponNumber;
        
        if(!photonView.IsMine)
            return;
        
        Hashtable table = new Hashtable();
        table.Add("armor",CurrentArmor);
        table.Add("color",CurrentColor);
        table.Add("weapon",CurrentWeapon);
        if(!PhotonNetwork.InRoom)
            return;
    
        PhotonNetwork.LocalPlayer.SetCustomProperties(table);      
   } 
}