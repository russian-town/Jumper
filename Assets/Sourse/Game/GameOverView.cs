using Lean.Localization;
using TMPro;
using UnityEngine;

namespace Sourse.Game
{
    public class GameOverView : UIElement
    {
        private string Completed = "COMPLETED";

        [SerializeField] private TMP_Text _progressText;

        public void ShowProgress(float progress)
        {
            string translatedCompleted = LeanLocalization.GetTranslationText(Completed);
            _progressText.text = $"{progress} % {translatedCompleted}";
        }
    }
}
