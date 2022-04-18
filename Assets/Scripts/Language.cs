using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class Language : MonoBehaviour
{

    public void Czech()
    {
        StartCoroutine(ChangeLanguage(0));
    }
    public void English()
    {
        StartCoroutine(ChangeLanguage(1));
    }
    public IEnumerator ChangeLanguage(int LanguageNumber)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[LanguageNumber];
    }
}
