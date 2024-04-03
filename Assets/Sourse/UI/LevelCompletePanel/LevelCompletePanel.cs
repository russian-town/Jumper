using System.Collections;
using Lean.Localization;
using Sourse.Constants;
using TMPro;
using UnityEngine;

namespace Sourse.UI.LevelCompletePanel
{
    public class LevelCompletePanel : UIElement
    {
        [SerializeField] private float _speed;
        [SerializeField] private TMP_Text _completeLevelText;

        public void SetText(int levelNumber)
        {
            string translatedLevel = LeanLocalization.GetTranslationText(TranslationText.Level);
            string translatedComplete = LeanLocalization.GetTranslationText(TranslationText.Completed);
            string translationText = $"{translatedLevel} {levelNumber} {translatedComplete}";
            _completeLevelText.text = translationText;
        }

    }
}
