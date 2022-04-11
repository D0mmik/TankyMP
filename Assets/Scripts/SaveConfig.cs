using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveConfig : MonoBehaviour
{
    public ArmorChanger armorChanger;
    public ColorChanger colorChanger;
    public PropulsionChanger propulsionChanger;
    public WeaponsChanger weaponsChanger;

    void Update()
    {
        if(Input.GetKey(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("armor", armorChanger.currentArmor);
        PlayerPrefs.SetInt("color", colorChanger.currentColor);
        //PlayerPrefs.SetInt("propulsion", propulsionChanger.currentPropulsion);
        PlayerPrefs.SetInt("weapon", weaponsChanger.currentWeapon);

    }
    public void Load()
    {
        armorChanger.dothis(PlayerPrefs.GetInt("armor"));
        colorChanger.dothis(PlayerPrefs.GetInt("color"));
        //propulsionChanger.dothis(PlayerPrefs.GetInt("propulsion"));
        weaponsChanger.dothis(PlayerPrefs.GetInt("weapon"));

    }
}
