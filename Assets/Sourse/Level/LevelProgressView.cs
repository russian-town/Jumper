using Sourse.Finish;
using Sourse.Player.Common.Scripts;
using Sourse.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.Level
{
    public class LevelProgressView : UIElement
    {
        [SerializeField] private Image _levelProgressBar;

        private LevelProgress _levelProgress;

        public void Initialize(PlayerInitializer playerInitializer,
            FinishPosition finishPosition)
            => _levelProgress = new LevelProgress(playerInitializer,
                finishPosition);

        public void UpdateProgressBar()
            => _levelProgressBar.fillAmount = _levelProgress.GetDistance();
    }
}
