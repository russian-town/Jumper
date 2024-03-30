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
            => _levelProgress = levelProgress;

        public void UpdateProgressBar()
            => _levelProgressBar.fillAmount = _levelProgress.GetDistance();
    }
}
