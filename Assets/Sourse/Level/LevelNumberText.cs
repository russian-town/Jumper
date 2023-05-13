using TMPro;
using UnityEngine;
using Lean.Localization;

public class LevelNumberText : MonoBehaviour
{
    private TMP_Text _text;

    public void Initialize(int levelNumber)
    {
        _text = GetComponent<TMP_Text>();
        _text.text = LeanLocalization.GetTranslationText("Level") + " " + levelNumber.ToString();
    }
}
