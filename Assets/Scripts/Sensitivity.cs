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

    public void LoadSliderValue()
    {
        ScrollSensSlider.value = PlayerPrefs.GetFloat("ScrollSens", 30);;
        SensSlider.value = PlayerPrefs.GetFloat("Sens", 40);

    }
    public void SaveSliderValue()
    {
        PlayerPrefs.SetFloat("ScrollSens", ScrollSensSlider.value);
        PlayerPrefs.SetFloat("Sens", SensSlider.value);
    }
    void Update()
    {
        ScrollSensText.text = (ScrollSensSlider.value * 100).ToString("F0");
        SensText.text = (SensSlider.value * 100).ToString("F0");
    }
}
