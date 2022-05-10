using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class Language : MonoBehaviour
{
    private string selectedlanguage;
    public void Czech()
    {
        StartCoroutine(ChangeLanguage(0));
        selectedlanguage = "cs";
    }
    public void English()
    {
        StartCoroutine(ChangeLanguage(1));
        selectedlanguage = "en";
    }
    public IEnumerator ChangeLanguage(int languageIndex)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
    }
    public void SaveLanguage()
    {
        PlayerPrefs.SetString("language",selectedlanguage);
    }
}
