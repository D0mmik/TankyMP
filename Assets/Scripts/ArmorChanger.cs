using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArmorChanger : MonoBehaviour
{
    public GameObject[] armors;
    public ButtonsPrefab Buttons;
    public GameObject currentclone;
    public int currentArmor;


    void Start()
    {

        for(int i = 0; i < armors.Length; i++)
        {   
            var ArmorButtonClone = Instantiate(Buttons, transform).GetComponent<ButtonsPrefab>();
            ArmorButtonClone.ButtonInt = i;
            ArmorButtonClone.Name.text = ($"ARMOR {ArmorButtonClone.ButtonInt}");  
            ArmorButtonClone.Button.onClick.AddListener(()=>dothis(ArmorButtonClone.ButtonInt));        
        }
    }
    public void dothis(int number)
    {  
        foreach( var item in armors)
        {
            item.SetActive(false);
        }
        armors[number].SetActive(true);
    }
}
