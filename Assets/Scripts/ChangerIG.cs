using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChangerIG : MonoBehaviourPun
{
    public Load Load;
    public ButtonsPrefab Buttons;
    public Transform WeaponHere;
    public Transform ArmorHere;
    public Transform ColorHere;

    public int CurrentArmor;
    public int CurrentColor;
    public int CurrentWeapon;

    void Start()
    {

        if(photonView.IsMine)
        {
            for(int i = 0; i < Load.weapons.Length; i++)
            {   
                var WeaponsButtonClone = Instantiate(Buttons, WeaponHere.transform).GetComponent<ButtonsPrefab>();
                WeaponsButtonClone.ButtonInt = i;
                WeaponsButtonClone.Name.text = ($"WEAPON {WeaponsButtonClone.ButtonInt}");  
                WeaponsButtonClone.Button.onClick.AddListener(()=>DoWeapon(WeaponsButtonClone.ButtonInt));        
            }

            for(int i = 0; i < Load.armor.Length; i++)
            {   
                var ArmorButtonClone = Instantiate(Buttons, ArmorHere.transform).GetComponent<ButtonsPrefab>();
                ArmorButtonClone.ButtonInt = i;
                ArmorButtonClone.Name.text = ($"ARMOR {ArmorButtonClone.ButtonInt}");  
                ArmorButtonClone.Button.onClick.AddListener(()=>DoArmor(ArmorButtonClone.ButtonInt));        
            }


            for(int i = 0; i < Load.color.Length; i++)
            {   
                var ColorButtonClone = Instantiate(Buttons, ColorHere.transform).GetComponent<ButtonsPrefab>();
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
            CurrentWeapon = number;
            PlayerPrefs.SetInt("weapon", CurrentWeapon);
            Load.UpdateConfig();
        }
    }
    public void DoArmor(int number)
    {
        if(photonView.IsMine)
        {
            CurrentArmor = number;
            PlayerPrefs.SetInt("armor", CurrentArmor); 
            Load.UpdateConfig();
        }
    }
    public void DoColor(int number)
    {
        if(photonView.IsMine)
        {
            CurrentColor = number;
            PlayerPrefs.SetInt("color", CurrentColor);
            Load.UpdateConfig();
        }
    }
    
}
