using System;
using Lean.Localization;
using Sourse.Constants;
using Sourse.Game.FinishContent;
using TMPro;
using UnityEngine;

namespace Sourse.UI.LevelCompletePanel
{
    public class LevelFinishView : UIElement
    {
        [SerializeField] private float _speed;
        [SerializeField] private TMP_Text _completeLevelText;
        [SerializeField] private NextLevelButton _nextLevelButton;

        private Finish _finish;

        public event Action NextLevelButtonClicked;

        public void Initialize(Finish finish)
        {
            _finish = finish;
            _nextLevelButton.Initialize();
            Hide();
        }

        public void Subscribe()
        {
            _finish.LevelCompleted += OnLevelComplete;
            _nextLevelButton.AddListener(()
                => NextLevelButtonClicked?.Invoke());
        }

        public void Unsubscribe()
        {
            _finish.LevelCompleted -= OnLevelComplete;
            _nextLevelButton.RemoveListener(()
                => NextLevelButtonClicked?.Invoke());
        }

        public void SetText(int levelNumber)
        {
            string translatedLevel = LeanLocalization.GetTranslationText(TranslationText.Level);
            string translatedComplete = LeanLocalization.GetTranslationText(TranslationText.Completed);
            string translationText = $"{translatedLevel} {levelNumber} {translatedComplete}";
            _completeLevelText.text = translationText;
        }

        private void OnLevelComplete()
        {
            SetText(1);
            Show();
        }
    }
}
