using System;
using Lean.Localization;
using Sourse.Constants;
using Sourse.Player.Common;
using TMPro;
using UnityEngine;

namespace Sourse.UI.LevelCompletePanel
{
    public class LevelFinishView : UIElement
    {
        [SerializeField] private float _speed;
        [SerializeField] private TMP_Text _completeLevelText;
        [SerializeField] private NextLevelButton _nextLevelButton;

        private LevelFinisher _levelFinisher;

        public event Action NextLevelButtonClicked;

        public void Initialize(LevelFinisher levelFinisher)
        {
            _levelFinisher = levelFinisher;
            _nextLevelButton.Initialize();
        }

        public void Subscribe()
        {
            _levelFinisher.LevelCompleted += OnLevelComplete;
            _nextLevelButton.AddListener(()
                => NextLevelButtonClicked?.Invoke());
        }

        public void Unsubscribe() 
        {
            _levelFinisher.LevelCompleted -= OnLevelComplete;
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

        private void OnLevelComplete(int levelNumber)
        {
            SetText(levelNumber);
        }
    }
}
