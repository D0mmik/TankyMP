using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArmorChanger : MonoBehaviour
{
    public GameObject[] Armors;
    public ButtonsPrefab Buttons;
    public int CurrentArmor;


    void Start()
    {

        for(int i = 0; i < Armors.Length; i++)
        {   
            var ArmorButtonClone = Instantiate(Buttons, transform).GetComponent<ButtonsPrefab>();
            ArmorButtonClone.ButtonInt = i;
            ArmorButtonClone.Name.text = ($"ARMOR {ArmorButtonClone.ButtonInt}");  
            ArmorButtonClone.Button.onClick.AddListener(()=>dothis(ArmorButtonClone.ButtonInt));        
        }
    }
    public void dothis(int number)
    {  
        foreach( var item in Armors)
        {
            item.SetActive(false);
        }
        Armors[number].SetActive(true);
        CurrentArmor = number;
    }
}
