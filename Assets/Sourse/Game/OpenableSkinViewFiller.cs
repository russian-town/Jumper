using System.Collections.Generic;
using Sourse.Constants;
using Sourse.Finish;
using Sourse.Player.Common.Scripts;
using Sourse.Save;
using Sourse.UI.LevelCompletePanel;
using Sourse.UI.Shop.SkinConfiguration;
using UnityEngine;

namespace Sourse.Game
{
    public class OpenableSkinViewFiller : IDataReader, IDataWriter
    {
        private readonly LevelCompletePanel _levelCompletePanel;
        private readonly GroundDetector _groundDetector;
        private readonly List<SkinConfig> _skinConfigs = new();

        private float _currentCompletePercent;

        public OpenableSkinViewFiller(LevelCompletePanel levelCompletePanel,
            List<SkinConfig> skinConfigs,
            GroundDetector groundDetector)
        {
            _levelCompletePanel = levelCompletePanel;
            _skinConfigs = skinConfigs;
            _groundDetector = groundDetector;
            _levelCompletePanel.HideOpeningSkinBar();
        }

        public void Subscribe()
            => _groundDetector.Fell += OnFell;

        public void Unsubscribe()
            => _groundDetector.Fell -= OnFell;

        public void Read(PlayerData playerData)
        {
            _currentCompletePercent = playerData.OpenableSkinSaveDatas[1].Persent;
            Debug.Log(_currentCompletePercent);
            int id = playerData.OpenableSkinSaveDatas[1].ID;
            _levelCompletePanel.Initialize(_currentCompletePercent, _skinConfigs[id]);
        }

        public void Write(PlayerData playerData)
        {
            playerData.OpenableSkinSaveDatas[1].Persent =
                _currentCompletePercent + PlayerParameter.PercentPerLevel;
        }

        private void OnFell(Collision collision)
        {
            if(collision.transform.TryGetComponent(out LevelCompleteSoundPlayer _))
                CalculatePercent();
        }

        private void CalculatePercent()
        {
            float targetFillAmount;
            targetFillAmount = _currentCompletePercent + PlayerParameter.PercentPerLevel;
            _levelCompletePanel.StartFillSkinBarCoroutine(targetFillAmount);
        }
    }
}
