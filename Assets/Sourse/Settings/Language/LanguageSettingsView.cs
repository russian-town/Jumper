using UnityEngine;
using UnityEngine.UI;

public class LanguageSettingsView : MonoBehaviour
{
    [SerializeField] private Image _languageImage;
    [SerializeField] private Sprite _rusLangugeSprite;
    [SerializeField] private Sprite _engLangugeSprite;
    [SerializeField] private Sprite _turLangugeSprite;

    public void SwitchLanguage(Language language)
    {
        switch (language)
        {
            case Language.Rus:
                _languageImage.sprite = _rusLangugeSprite;
                break;
            case Language.Eng:
                _languageImage.sprite = _engLangugeSprite;
                break;
            case Language.Tur:
                _languageImage.sprite = _turLangugeSprite;
                break;
        }
    }
}
