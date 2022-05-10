using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsChanger : MonoBehaviour
{
    public GameObject[] Weapons;
    public ButtonsPrefab Buttons;
    public GameObject CurrentClone;
    public int CurrentWeapon;

    public Load load;


    void Start()
    {

        for(int i = 0; i < Weapons.Length; i++)
        {   
            var WeaponsButtonClone = Instantiate(Buttons, transform).GetComponent<ButtonsPrefab>();
            WeaponsButtonClone.ButtonInt = i;
            WeaponsButtonClone.Name.text = ($"WEAPON {WeaponsButtonClone.ButtonInt}");  
            WeaponsButtonClone.Button.onClick.AddListener(()=>dothis(WeaponsButtonClone.ButtonInt));        
        }
    }
    public void dothis(int number)
    {  
        foreach( var item in Weapons)
        {
            item.SetActive(false);
        }
        Weapons[number].SetActive(true);
        CurrentWeapon = number;
    }
}
