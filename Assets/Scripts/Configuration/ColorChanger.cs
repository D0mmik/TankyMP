using System;
using UI;
using UnityEngine;

namespace Configuration
{
    public class ColorChanger : MonoBehaviour
    {
        [SerializeField] Material[] Colors;
        [SerializeField] ButtonsPrefab Buttons;
        [SerializeField] GameObject Tank;
        public int CurrentColor;

        void Start()
        {
            for(int i = 0; i < Colors.Length; i++)         
                SpawnButton(i, DoColor, "COLOR", transform);                   
        }
    
        void SpawnButton(int index, Action<int> onClick, string buttonName, Transform parent)
        {
            var button = Instantiate(Buttons, transform);
            button.Set(index: index,
                name: $"{buttonName} {index}",
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
}