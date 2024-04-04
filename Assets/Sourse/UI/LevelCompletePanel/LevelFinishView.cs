using System;
using Lean.Localization;
using Sourse.Constants;
using Sourse.Level;
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

        private PlayerFinisher _playerFinisher;
        private LevelLoader _levelLoader;

        public event Action NextLevelButtonClicked;

        public void Initialize(PlayerFinisher levelFinisher,
            LevelLoader levelLoader)
        {
            _playerFinisher = levelFinisher;
            _levelLoader = levelLoader;
            _nextLevelButton.Initialize();
            Hide();
        }

        public void Subscribe()
        {
            _playerFinisher.LevelCompleted += OnLevelComplete;
            _nextLevelButton.AddListener(()
                => NextLevelButtonClicked?.Invoke());
        }

        public void Unsubscribe() 
        {
            _playerFinisher.LevelCompleted -= OnLevelComplete;
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
            SetText(_levelLoader.GetCurrentNumber());
            Show();
        }
    }
}
