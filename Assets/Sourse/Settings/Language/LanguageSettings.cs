using UnityEngine;
using TMPro;
using Lean.Localization;

[RequireComponent(typeof(LanguageSettingsView), typeof(Saver))]
public class LanguageSettings : MonoBehaviour
{
    private const string LanguageKey = "LanguageKey";
    private const string RussianLanguage = "Russian";
    private const string EnglishLanguage = "English";
    private const string TurkishLanguage = "Turkish";

    [SerializeField] private TMP_Dropdown _languageDropdown;
    [SerializeField] private LeanLocalization _leanLocalization;

    private LanguageSettingsView _languageSettingsView;
    private Saver _saver;

    private void Awake()
    {
        _languageSettingsView = GetComponent<LanguageSettingsView>();
        _saver = GetComponent<Saver>();

        if (_saver.TryGetValue(LanguageKey, out int value))
        {
            _languageDropdown.SetValueWithoutNotify(value);
            OnLanguageChanged(value);
        }
    }

    private void OnEnable() => _languageDropdown.onValueChanged.AddListener(OnLanguageChanged);

    private void OnDisable() => _languageDropdown.onValueChanged.RemoveListener(OnLanguageChanged);


    private void OnLanguageChanged(int value)
    {
        switch (value)
        {
            case 0:
                SwitchLanguage(Language.Rus, RussianLanguage);
                break;
            case 1:
                SwitchLanguage(Language.Eng, EnglishLanguage);
                break;
            case 2:
                SwitchLanguage(Language.Tur, TurkishLanguage);
                break;
        }

        _saver.Save(LanguageKey, value);
    }

    private void SwitchLanguage(Language language, string languageName)
    {
        _languageSettingsView.SwitchLanguage(language);
        _leanLocalization.SetCurrentLanguage(languageName);
        _saver.SaveLanguage(language);
    }
}

public enum Language
{
    Rus,
    Eng,
    Tur
}
