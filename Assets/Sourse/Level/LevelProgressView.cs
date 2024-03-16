using Sourse.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.Level
{
    public class LevelProgressView : UIElement
    {
        [SerializeField] private Image _levelProgressBar;

        public void UpdateProgressBar(float value)
        {
            _levelProgressBar.fillAmount = value;
        }
    }
}
