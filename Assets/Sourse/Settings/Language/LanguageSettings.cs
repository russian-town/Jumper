using UnityEngine;
using Lean.Localization;

public class LanguageSettings : MonoBehaviour
{
    [SerializeField] private LeanLocalization _leanLocalization;

    public void SetLanguage(string languageName)
    {
        _leanLocalization.SetCurrentLanguage(languageName);
    }
}

public enum Language
{
    Rus,
    Eng,
    Tur
}
