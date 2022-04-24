using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sensitivity : MonoBehaviour
{
    public Slider scrollSensSlider;
    public TMP_Text scrollSensText;
    public Slider sensSlider;
    public TMP_Text sensText;

    public void LoadSliderValue()
    {
        scrollSensSlider.value = PlayerPrefs.GetFloat("ScrollSens", 30);;
        sensSlider.value = PlayerPrefs.GetFloat("Sens", 40);

    }
    public void SaveSliderValue()
    {
        PlayerPrefs.SetFloat("ScrollSens", scrollSensSlider.value);
        PlayerPrefs.SetFloat("Sens", sensSlider.value);
    }
    void Update()
    {
        scrollSensText.text = (scrollSensSlider.value * 100).ToString("F0");
        sensText.text = (sensSlider.value * 100).ToString("F0");
    }
}
