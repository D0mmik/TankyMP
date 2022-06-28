using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Language
{
    public class Language : MonoBehaviour
    {
        string selectedLanguage;
        public void Czech()
        {
            StartCoroutine(ChangeLanguage(0));
            selectedLanguage = "cs";
        }
        public void English()
        {
            StartCoroutine(ChangeLanguage(1));
            selectedLanguage = "en";
        }

        private IEnumerator ChangeLanguage(int languageIndex)
        {
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
        }
        public void SaveLanguage()
        {
            PlayerPrefs.SetString("language",selectedLanguage);
        }
    }
}
