using System.Collections;
using Agava.YandexGames;
using Sourse.Settings.Language;
using UnityEngine;

public class YandexInit : MonoBehaviour
{
    private const string English = "English";
    private const string Russian = "Russian";
    private const string Turkish = "Turkish";
    private const string Tr = "tr";
    private const string En = "en";
    private const string Ru = "ru";

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
            case Tr:
                _languageSettings.SetLanguage(Turkish);
                break;
            case Ru:
                _languageSettings.SetLanguage(Russian);
                break;
            case En:
                _languageSettings.SetLanguage(English);
                break;
            default:
                _languageSettings.SetLanguage(Russian);
                break;
        }
    }
}
