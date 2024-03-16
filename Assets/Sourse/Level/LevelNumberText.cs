using Lean.Localization;
using TMPro;
using UnityEngine;

namespace Sourse.Level
{
    public class LevelNumberText : MonoBehaviour
    {
        private const string Level = "Level";

        private TMP_Text _text;

        public void Initialize(int levelNumber)
        {
            _text = GetComponent<TMP_Text>();
            string translatedLevel = LeanLocalization.GetTranslationText(Level);
            _text.text = $"{translatedLevel} {levelNumber}";
        }
    }
}
