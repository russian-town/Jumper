using Sourse.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.Level
{
    public class LevelProgressView : UIElement
    {
        [SerializeField] private Image _levelProgressBar;

        private LevelProgress _levelProgress;

        public void Initialize(LevelProgress levelProgress)
        {
            _levelProgress = levelProgress;
            _levelProgressBar.fillAmount = _levelProgress.GetCurrentDistance();
        }

        public void UpdateProgressBar()
        {
            if (_levelProgress.GetCurrentDistance() < _levelProgressBar.fillAmount)
                return;

            _levelProgressBar.fillAmount = _levelProgress.GetCurrentDistance();
        }
    }
}
