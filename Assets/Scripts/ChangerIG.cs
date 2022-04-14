using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChangerIG : MonoBehaviourPun
{
    public Load load;
    public ButtonsPrefab buttons;
    public Transform weaponHere;
    public Transform armorHere;
    public Transform colorHere;

    public int currentArmor;
    public int currentColor;
    public int currentWeapon;

    void Start()
    {

        if(photonView.IsMine)
        {
            for(int i = 0; i < load.weapons.Length; i++)
        {   
            var WeaponsButtonClone = Instantiate(buttons, weaponHere.transform).GetComponent<ButtonsPrefab>();
            WeaponsButtonClone.ButtonInt = i;
            WeaponsButtonClone.Name.text = ($"WEAPON {WeaponsButtonClone.ButtonInt}");  
            WeaponsButtonClone.Button.onClick.AddListener(()=>DoWeapon(WeaponsButtonClone.ButtonInt));        
        }

        for(int i = 0; i < load.armor.Length; i++)
        {   
            var ArmorButtonClone = Instantiate(buttons, armorHere.transform).GetComponent<ButtonsPrefab>();
            ArmorButtonClone.ButtonInt = i;
            ArmorButtonClone.Name.text = ($"ARMOR {ArmorButtonClone.ButtonInt}");  
            ArmorButtonClone.Button.onClick.AddListener(()=>DoArmor(ArmorButtonClone.ButtonInt));        
        }
        for(int i = 0; i < load.color.Length; i++)
        {   
            var ColorButtonClone = Instantiate(buttons, colorHere.transform).GetComponent<ButtonsPrefab>();
            ColorButtonClone.ButtonInt = i;
            ColorButtonClone.Name.text = ($"COLOR {ColorButtonClone.ButtonInt}");  
            ColorButtonClone.Button.onClick.AddListener(()=>DoColor(ColorButtonClone.ButtonInt));                    
        }
        }
    }
    public void DoWeapon(int number)
    {  
        if(photonView.IsMine)
        {
            currentWeapon = number;
            PlayerPrefs.SetInt("weapon", currentWeapon);
            load.UpdateConfig();
        }
    }
    public void DoArmor(int number)
    {
        if(photonView.IsMine)
        {
            currentArmor = number;
            PlayerPrefs.SetInt("armor", currentArmor);
            load.UpdateConfig();
        }
    }
    public void DoColor(int number)
    {
        if(photonView.IsMine)
        {
            currentColor = number;
            PlayerPrefs.SetInt("color", currentColor);
            load.UpdateConfig();
        }
    }
    
}
