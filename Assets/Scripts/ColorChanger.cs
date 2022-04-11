using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material[] colors;
    public ButtonsPrefab Buttons;
    public GameObject tank;
    public int currentColor;

    void Start()
    {
        FindTank();
        for(int i = 0; i < colors.Length; i++)
        {   
            var ColorButtonClone = Instantiate(Buttons, transform).GetComponent<ButtonsPrefab>();
            ColorButtonClone.ButtonInt = i;
            ColorButtonClone.Name.text = ($"COLOR {ColorButtonClone.ButtonInt}");  
            ColorButtonClone.Button.onClick.AddListener(()=>dothis(ColorButtonClone.ButtonInt));                    
        }
    }
    public void dothis(int number)
    {  
        currentColor = number;
        tank.GetComponent<MeshRenderer>().material = colors[number];

    }
    public void FindTank()
    {
        tank = GameObject.Find("Body");   
    }
}
