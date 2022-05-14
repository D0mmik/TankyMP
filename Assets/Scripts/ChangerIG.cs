using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class ChangerIG : MonoBehaviourPun
{
    public Load Load;
    public ButtonsPrefab Buttons;
    private OneBarrel oneBarrel;
    public Transform WeaponHere;
    public Transform ArmorHere;
    public Transform ColorHere;

    public int CurrentArmor;
    public int CurrentColor;
    public int CurrentWeapon;

    void Start()
    {

        if(!photonView.IsMine)
            return;
        
        for(int i = 0; i < Load.Weapons.Length; i++)
            SpawnButton(i, (index)=> DoWeapon(index), "WEAPON", WeaponHere.transform);

        for(int i = 0; i < Load.Armor.Length; i++)
            SpawnButton(i, (index)=> DoArmor(index), "ARMOR", ArmorHere.transform);


        for(int i = 0; i < Load.Color.Length; i++)  
            SpawnButton(i, (index)=> DoColor(index), "COLOR", ColorHere.transform);                    
        
        
    }
    void SpawnButton(int index, Action<int> onClick, string name, Transform parent)
    {
        var button = Instantiate(Buttons, parent);
        button.Set(index: index,
                   name: $"{name}{index}",
                   callback: () => onClick(index));
    }
    public void DoWeapon(int index)
    {  
        if(!photonView.IsMine)
            return;
        if(index < 0 || index > Load.Weapons.Length)
            return;
            CurrentWeapon = index;
        SaveProperty("weapon", index); 
        
    }
    public void DoArmor(int index)
    {
        if(!photonView.IsMine)
            return;
        if(index < 0 || index > Load.Armor.Length)
            return;
        
        CurrentArmor = index;
        SaveProperty("armor", index); 
        
    }
    public void DoColor(int index)
    {
        if(!photonView.IsMine)
            return;
        if(index < 0 || index > Load.Color.Length)
            return;
        
        CurrentColor = index;
        SaveProperty("color", index); 
    }
    void SaveProperty(string property, int state)
    {
        PlayerPrefs.SetInt(property, state);
        Load.UpdateConfig();
    }
    
}
