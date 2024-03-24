using System.Collections;
using Agava.YandexGames;
using Sourse.Constants;
using Sourse.Settings.Language;
using UnityEngine;

namespace Sourse.YandexAds
{
    public class YandexInit : MonoBehaviour
    {
        [SerializeField] private LanguageSettings _languageSettings;

        private IEnumerator Start()
        {
#if UNITY_EDITOR
            yield break;
#endif
            yield return YandexGamesSdk.Initialize();

            if (_languageSettings == null)
                yield break;

            switch (YandexGamesSdk.Environment.i18n.lang)
            {
                case LanguagueName.Tr:
                    _languageSettings.SetLanguage(LanguagueName.Turkish);
                    break;
                case LanguagueName.Ru:
                    _languageSettings.SetLanguage(LanguagueName.Russian);
                    break;
                case LanguagueName.En:
                    _languageSettings.SetLanguage(LanguagueName.English);
                    break;
                default:
                    _languageSettings.SetLanguage(LanguagueName.Russian);
                    break;
            }
        }
    }
}
