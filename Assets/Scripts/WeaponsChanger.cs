using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponsChanger : MonoBehaviour
{
    public GameObject[] Weapons;
    public ButtonsPrefab Buttons;
    public GameObject CurrentClone;
    public int CurrentWeapon;

    void Start()
    {
        for(int i = 0; i < Weapons.Length; i++)
            SpawnButton(i, (index)=> DoWeapon(index), "WEAPON", transform);          
    }
    void SpawnButton(int index, Action<int> onClick, string name, Transform parent)
    {
        var button = Instantiate(Buttons, transform);
        button.Set(index: index,
                   name: $"{name}{index}",
                   callback: () => onClick(index));
    }
    public void DoWeapon(int index)
    {  
        if(index < 0 || index > Weapons.Length)      
            return;

        foreach( var item in Weapons)
            item.SetActive(false);

        Weapons[index].SetActive(true);
        CurrentWeapon = index;
    }
}
