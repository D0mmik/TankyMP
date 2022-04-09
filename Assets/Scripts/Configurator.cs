using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Configurator : MonoBehaviour
{
    [SerializeField] private GameObject[] windows;
    public void CloseWindows()
    {
        foreach( var item in windows)
        {
            item.SetActive(false);
        }
    }
    public void ToggleWindow(string nameOfWindow)
    {
        CloseWindows();
        windows.Single((x)=> x.name == nameOfWindow).SetActive(true);
    }
    public void WeaponsButton()
    {
        ToggleWindow("Weapons");
    }
    public void PropulsionButton()
    {
        ToggleWindow("Propulsion");
    }
    public void ArmorButton()
    {
        ToggleWindow("Armor");
    }
    public void ColorButton()
    {
        ToggleWindow("Color");
    }
}
