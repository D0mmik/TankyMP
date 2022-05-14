using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorChanger : MonoBehaviour
{
    public Material[] Colors;
    public ButtonsPrefab Buttons;
    public GameObject Tank;
    public int CurrentColor;

    void Start()
    {
        for(int i = 0; i < Colors.Length; i++)         
            SpawnButton(i, (index)=> DoColor(index), "COLOR", transform);                   
    }
    
    void SpawnButton(int index, Action<int> onClick, string name, Transform parent)
    {
        var button = Instantiate(Buttons, transform);
        button.Set(index: index,
                   name: $"{name}{index}",
                   callback: () => onClick(index));
    }
    public void DoColor(int index)
    {  
        if(index < 0 || index > Colors.Length)      
            return;
        
        CurrentColor = index;
        Tank.GetComponent<MeshRenderer>().material = Colors[index];
    }
}