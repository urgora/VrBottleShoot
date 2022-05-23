using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleLocalization
{
    public class LanguageManager : MonoBehaviour
    {
        public static LanguageManager _instance;
        public void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            LocalizationManager.Read();
            SystemLanguage T = Application.systemLanguage;
            //SystemLanguage T = SystemLanguage.French;
            changelanguage(T);
        }

        private void changelanguage(SystemLanguage T)
        { 
            LocalizationManager.FONTID = 2;
            switch (T)
            {
                case SystemLanguage.German:
                    LocalizationManager.Language = "German";
                    break;
                case SystemLanguage.Spanish:
                    LocalizationManager.Language = "Spanish";
                    break;
                case SystemLanguage.Russian:
                    LocalizationManager.Language = "Russian";
                    LocalizationManager.FONTID = 1;
                    break;
                case SystemLanguage.Turkish:
                    LocalizationManager.Language = "Turkish";
                    LocalizationManager.FONTID = 0;
                    break;
                case SystemLanguage.Portuguese:
                    LocalizationManager.Language = "Portuguese";
                    break;
                case SystemLanguage.Italian:
                    LocalizationManager.Language = "Italian";
                    break;
                case SystemLanguage.Polish:
                    LocalizationManager.Language = "Polish";
                    LocalizationManager.FONTID = 0;
                    break;
                case SystemLanguage.Dutch:
                    LocalizationManager.Language = "Dutch";
                    break;
                case SystemLanguage.French:
                    LocalizationManager.Language = "French";
                    break;
                default:
                    LocalizationManager.Language = "English";
                    LocalizationManager.FONTID = 2;
                    break;
            }

        }

        public void SetLocalization(SystemLanguage localization)
        {
            changelanguage(localization);
        }

    }
}