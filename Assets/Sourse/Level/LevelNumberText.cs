using Lean.Localization;
using Sourse.Constants;
using TMPro;
using UnityEngine;

namespace Sourse.Level
{
    public class LevelNumberText : MonoBehaviour
    {
        private TMP_Text _text;

        public void Initialize(int levelNumber)
        {
            _text = GetComponent<TMP_Text>();
            string translatedLevel =
                LeanLocalization.GetTranslationText(TranslationText.Level);
            _text.text = $"{translatedLevel} {levelNumber}";
        }
    }
}
