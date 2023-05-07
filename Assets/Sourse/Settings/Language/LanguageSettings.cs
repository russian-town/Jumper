using UnityEngine;
using TMPro;
[RequireComponent(typeof(LanguageSettingsView), typeof(Saver))]
public class LanguageSettings : MonoBehaviour
{
    private const string LanguageKey = "LanguageKey";

    [SerializeField] private TMP_Dropdown _languageDropdown;

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
                LanguageChanged(Language.Rus);
                break;
            case 1:
                LanguageChanged(Language.Eng);
                break;
            case 2:
                LanguageChanged(Language.Tur);
                break;
        }

        _saver.Save(LanguageKey, value);
    }

    private void LanguageChanged(Language language)
    {
        _languageSettingsView.SwitchLanguage(language);
    }
}

public enum Language
{
    Rus,
    Eng,
    Tur
}
