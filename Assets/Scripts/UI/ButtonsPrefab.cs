using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonsPrefab : MonoBehaviour
    {
        public Button Button;
        public int Index;
        public TMP_Text Name;

        public void Set(int index, string name, Action callback)
        {
            Index = index;
            Name.text = name;
        
            Button.onClick.RemoveAllListeners();
            Button.onClick.AddListener(() => callback());
        }
    }
}
