using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootingScripts
{
    public class Sensitivity : MonoBehaviour
    {
        public Slider ScrollSensSlider;
        public TMP_Text ScrollSensText;
        public Slider SensSlider;
        public TMP_Text SensText;

        const string KeyScrollSens = "ScrollSens"; 
        const string KeySens = "Sens";

        public void LoadSliderValue()
        {
            ScrollSensSlider.value = PlayerPrefs.GetFloat(KeyScrollSens, 30);;
            SensSlider.value = PlayerPrefs.GetFloat(KeySens, 40);

        }
        public void SaveSliderValue()
        {
            PlayerPrefs.SetFloat(KeyScrollSens, ScrollSensSlider.value);
            PlayerPrefs.SetFloat(KeySens, SensSlider.value);
        }
        void Update()
        {
            ScrollSensText.text = $"{ScrollSensSlider.value * 100:F0}";
            SensText.text = $"{SensSlider.value * 100:F0}";
        }
    }
}
