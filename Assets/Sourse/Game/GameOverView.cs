using Lean.Localization;
using Sourse.Constants;
using Sourse.UI;
using TMPro;
using UnityEngine;

namespace Sourse.Game
{
    public class GameOverView : UIElement
    {
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private RewardedPanel _rewardPanel;

        public void Subscribe()
        {
            _rewardPanel.Subscribe();
        }

        public void Unsubscribe() 
        {
            _rewardPanel.Unsubscribe();
        }

        public void Initialize()
        {
            _rewardPanel.Initialize();
        }

        public void ShowProgress(float progress)
        {
            string translatedCompleted = LeanLocalization.GetTranslationText(TranslationText.Completed);
            _progressText.text = $"{progress} % {translatedCompleted}";
        }
    }
}
