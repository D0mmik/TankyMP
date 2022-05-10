using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material[] Colors;
    public ButtonsPrefab Buttons;
    public GameObject Tank;
    public int CurrentColor;

    void Start()
    {
        FindTank();
        for(int i = 0; i < Colors.Length; i++)
        {   
            var ColorButtonClone = Instantiate(Buttons, transform).GetComponent<ButtonsPrefab>();
            ColorButtonClone.ButtonInt = i;
            ColorButtonClone.Name.text = ($"COLOR {ColorButtonClone.ButtonInt}");  
            ColorButtonClone.Button.onClick.AddListener(()=>dothis(ColorButtonClone.ButtonInt));                    
        }
    }
    public void dothis(int number)
    {  
        CurrentColor = number;
        Tank.GetComponent<MeshRenderer>().material = Colors[number];

    }
    public void FindTank()
    {
        Tank = GameObject.Find("Body");   
    }
}
