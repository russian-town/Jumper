using System.Collections.Generic;
using Sourse.Constants;
using Sourse.Save;
using Sourse.UI;
using Sourse.UI.Shop.SkinConfiguration;
using UnityEngine;

namespace Sourse.Game.Finish
{
    public class OpenableSkinViewFiller : IDataReader, IDataWriter
    {
        private readonly List<SkinConfig> _skinConfigs = new();
        private readonly OpenableSkinBar _openableSkinBar;

        private float _currentCompletePercent;
        private int _currentOpenableSkinID = -1;
        private List<OpenableSkinSaveData> _aviableOpenableSkinSaveData = new();

        public OpenableSkinViewFiller(List<SkinConfig> skinConfigs,
            OpenableSkinBar openableSkinBar)
        {
            _skinConfigs = skinConfigs;
            _openableSkinBar = openableSkinBar;
        }

        public void Read(PlayerData playerData)
        {
            foreach (var openableSkinSaveData in playerData.OpenableSkinSaveDatas)
                if (openableSkinSaveData.Persent < PlayerParameter.MaxPercent)
                    _aviableOpenableSkinSaveData.Add(openableSkinSaveData);

            if (_aviableOpenableSkinSaveData.Count == 0)
            {
                _openableSkinBar.Hide();
                return;
            }

            if (playerData.CurrentOpenableSkinID > 0)
            {
                _currentOpenableSkinID = playerData.CurrentOpenableSkinID;

                foreach (var openableSkinSaveData in playerData.OpenableSkinSaveDatas)
                {
                    if (openableSkinSaveData.ID == _currentOpenableSkinID)
                    {
                        _currentCompletePercent = openableSkinSaveData.Persent;
                        break;
                    }
                }

                InitializeOpenableSkinBar();
                return;
            }

            int index = Random.Range(PlayerParameter.MinIndex, _aviableOpenableSkinSaveData.Count);
            _currentOpenableSkinID = _aviableOpenableSkinSaveData[index].ID;
            InitializeOpenableSkinBar();
        }

        public void Write(PlayerData playerData)
        {
            foreach (var openableSkinSaveData in playerData.OpenableSkinSaveDatas)
            {
                if (openableSkinSaveData.ID == _currentOpenableSkinID)
                {
                    openableSkinSaveData.Persent = _currentCompletePercent;
                    break;
                }
            }

            if (_currentCompletePercent >= PlayerParameter.MaxPercent)
            {
                _currentCompletePercent = PlayerParameter.MaxPercent;
                _currentOpenableSkinID = -1;
                return;
            }

            playerData.CurrentOpenableSkinID = _currentOpenableSkinID;
        }

        private void InitializeOpenableSkinBar()
        {
            foreach (var skinConfig in _skinConfigs)
            {
                if (skinConfig.ID == _currentOpenableSkinID)
                {
                    _openableSkinBar.Initialize(skinConfig.Icon,
                        _currentCompletePercent);
                    break;
                }
            }
        }

        public void CalculatePercent()
        {
            _currentCompletePercent += PlayerParameter.MaxPercent;

            if (_currentOpenableSkinID < 0)
            {
                _openableSkinBar.Hide();
                return;
            }

            _openableSkinBar.Show();
            _openableSkinBar.StartFillSkinBarCoroutine(_currentCompletePercent);
        }
    }
}
