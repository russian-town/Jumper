using Sourse.Save;
using Sourse.UI.LevelCompletePanel;
using Sourse.UI.Shop.SkinConfiguration;

namespace Sourse.Game
{
    public class OpenableSkinViewFiller : IDataReader
    {
        private readonly LevelCompletePanel _levelCompletePanel;

        private Skin _skin;

        public void Initialize()
            => _levelCompletePanel.HideOpeningSkinBar();

        public void Read(PlayerData playerData)
        {
        }

        private void CalculatePercent()
        {
            if (_skin == null)
                return;

            float targetFillAmount;

            targetFillAmount = _levelCompletePanel.CurrentFillAmount /*+ _skin.CurrentOpenPercentage*/;
            _levelCompletePanel.Initialize(_skin);
            _levelCompletePanel.StartFillSkinBarCoroutine(targetFillAmount);
        }
    }
}
