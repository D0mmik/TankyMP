using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveConfig : MonoBehaviour
{
    public ArmorChanger ArmorChanger;
    public ColorChanger ColorChanger;
    public WeaponsChanger WeaponsChanger;
    void Start()
    {
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("armor", ArmorChanger.CurrentArmor);
        PlayerPrefs.SetInt("color", ColorChanger.CurrentColor);
        PlayerPrefs.SetInt("weapon", WeaponsChanger.CurrentWeapon);

    }
    public void Load()
    {
        ArmorChanger.dothis(PlayerPrefs.GetInt("armor"));
        ColorChanger.FindTank();
        ColorChanger.dothis(PlayerPrefs.GetInt("color"));
        WeaponsChanger.dothis(PlayerPrefs.GetInt("weapon"));

    }
}
