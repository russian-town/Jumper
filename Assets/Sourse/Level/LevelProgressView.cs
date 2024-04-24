using Sourse.Game.FinishContent;
using Sourse.Ground;
using Sourse.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.Level
{
    public class LevelProgressView : UIElement
    {
        [SerializeField] private Image _fillImage;

        private LevelProgress _levelProgress;
        private Finish _finish;
        private DeadZone _deadZone;

        public void Subscribe()
        {
            _finish.LevelCompleted += OnLevelCompleted;
            _deadZone.LevelLost += OnLevelLost;
        }

        public void Unsubscribe()
        {
            _finish.LevelCompleted -= OnLevelCompleted;
            _deadZone.LevelLost -= OnLevelLost;
        }

        public void Initialize(
            LevelProgress levelProgress,
            Finish finish,
            DeadZone deadZone)
        {
            _levelProgress = levelProgress;
            _finish = finish;
            _deadZone = deadZone;
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

        private void OnLevelLost()
            => Hide();
    }
}
