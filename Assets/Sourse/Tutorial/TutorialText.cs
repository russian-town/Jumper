using UnityEngine;
using TMPro;
using Lean.Localization;

[RequireComponent(typeof(TMP_Text))]
public class TutorialText : UIElement
{
    private const string Tap = "TAP";

    private TMP_Text _text;

    public void Initialize()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = $"{LeanLocalization.GetTranslationText(Tap)}";
    }
}
