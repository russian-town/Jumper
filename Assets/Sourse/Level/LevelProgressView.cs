using Sourse.Player.Common;
using Sourse.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.Level
{
    public class LevelProgressView : UIElement
    {
        [SerializeField] private Image _fillImage;

        private LevelProgress _levelProgress;
        private PlayerFinisher _playerFinisher;
        private PlayerDeath _playerDeath;

        public void Subscribe()
        {
            _playerFinisher.LevelCompleted += OnLevelCompleted;
            _playerDeath.Died += OnPlayerDied;
        }

        public void Unsubscribe()
        {
            _playerFinisher.LevelCompleted -= OnLevelCompleted;
            _playerDeath.Died -= OnPlayerDied;
        }

        public void Initialize(
            LevelProgress levelProgress,
            PlayerFinisher playerFinisher,
            PlayerDeath playerDeath)
        {
            _levelProgress = levelProgress;
            _playerFinisher = playerFinisher;
            _playerDeath = playerDeath;
            _fillImage.fillAmount = _levelProgress.GetCurrentDistance();
        }

        public void UpdateProgressBar()
        {
            if (gameObject.activeSelf == false)
                return;

            if (_levelProgress.GetCurrentDistance() < _fillImage.fillAmount)
                return;

            _fillImage.fillAmount = _levelProgress.GetCurrentDistance();
        }

        private void OnLevelCompleted()
            => Hide();

        private void OnPlayerDied()
            => Hide();
    }
}
