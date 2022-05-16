using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ArmorChanger : MonoBehaviour
{
    public GameObject[] Armors;
    public ButtonsPrefab Buttons;
    public int CurrentArmor;
    
    void Start()
    {
        for(int i = 0; i < Armors.Length; i++)     
            SpawnButton(i, (index)=> DoArmor(index), "ARMOR", transform); 
    }

    void SpawnButton(int index, Action<int> onClick, string name, Transform parent)
    {
        var button = Instantiate(Buttons, transform);
        button.Set(index: index,
                   name: $"{name}{index}",
                   callback: () => onClick(index));
    }

    public void DoArmor(int index)
    {  
        if(index < 0 || index > Armors.Length)      
            return;
        
        foreach( var item in Armors)
            item.SetActive(false);
            
        Armors[index].SetActive(true);
        CurrentArmor = index;
    }
}
