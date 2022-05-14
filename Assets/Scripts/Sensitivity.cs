using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sensitivity : MonoBehaviour
{
    public Slider ScrollSensSlider;
    public TMP_Text ScrollSensText;
    public Slider SensSlider;
    public TMP_Text SensText;

    private const string Key_ScrollSens = "ScrollSens";
    private const string Key_Sens = "Sens";

    public void LoadSliderValue()
    {
        ScrollSensSlider.value = PlayerPrefs.GetFloat(Key_ScrollSens, 30);;
        SensSlider.value = PlayerPrefs.GetFloat(Key_Sens, 40);

    }
    public void SaveSliderValue()
    {
        PlayerPrefs.SetFloat(Key_ScrollSens, ScrollSensSlider.value);
        PlayerPrefs.SetFloat(Key_Sens, SensSlider.value);
    }
    void Update()
    {
        ScrollSensText.text = $"{ScrollSensSlider.value * 100:F0}";
        SensText.text = $"{SensSlider.value * 100:F0}";
    }
}
