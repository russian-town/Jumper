using Lean.Localization;
using TMPro;
using UnityEngine;

namespace Sourse.Game
{
    public class GameOverView : UIElement
    {
        [SerializeField] private TMP_Text _progressText;

        public void ShowProgress(float progress)
        {
            _progressText.text = $"{progress} % {LeanLocalization.GetTranslationText("COMPLETED")}";
        }
    }
}
