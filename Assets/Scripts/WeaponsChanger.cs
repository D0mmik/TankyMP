using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsChanger : MonoBehaviour
{
    public GameObject[] weapons;
    public ButtonsPrefab Buttons;
    public GameObject currentclone;
    public int currentWeapon;


    void Start()
    {

        for(int i = 0; i < weapons.Length; i++)
        {   
            var WeaponsButtonClone = Instantiate(Buttons, transform).GetComponent<ButtonsPrefab>();
            WeaponsButtonClone.ButtonInt = i;
            WeaponsButtonClone.Name.text = ($"WEAPON {WeaponsButtonClone.ButtonInt}");  
            WeaponsButtonClone.Button.onClick.AddListener(()=>dothis(WeaponsButtonClone.ButtonInt));        
        }
    }
    public void dothis(int number)
    {  
        foreach( var item in weapons)
        {
            item.SetActive(false);
        }
        weapons[number].SetActive(true);
        currentWeapon = number;
    }
}
