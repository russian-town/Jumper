using System;
using System.Collections.Generic;
using Sourse.Constants;
using Sourse.Save;
using Sourse.UI;
using Sourse.UI.Shop.SkinConfiguration;
using UnityEngine;

namespace Sourse.Game.FinishContent
{
    public class OpenableSkinViewFiller : IDataReader, IDataWriter
    {
        private readonly List<SkinConfig> _skinConfigs = new ();
        private readonly OpenableSkinBar _openableSkinBar;
        private readonly List<OpenableSkinSaveData> _aviableOpenableSkinSaveData = new ();

        private float _currentCompletePercent;
        private int _currentOpenableSkinID = -1;

        public OpenableSkinViewFiller(List<SkinConfig> skinConfigs)
            => _skinConfigs = skinConfigs;

        public event Action<Sprite, float> Initialized;

        public event Action<float> PercentCalculated;

        public void Read(PlayerData playerData)
        {
            foreach (var openableSkinSaveData in playerData.OpenableSkinSaveDatas)
            {
                if (openableSkinSaveData.Persent < PlayerParameter.MaxPercent)
                {
                    _aviableOpenableSkinSaveData.Add(openableSkinSaveData);
                }
            }

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

                Initialize();
                return;
            }

            int index = UnityEngine.Random.Range(PlayerParameter.MinIndex, _aviableOpenableSkinSaveData.Count);
            _currentOpenableSkinID = _aviableOpenableSkinSaveData[index].ID;
            Initialize();
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

        public void CalculatePercent()
        {
            if (_currentOpenableSkinID < 0)
                return;

            _currentCompletePercent += PlayerParameter.PercentPerLevel;
            PercentCalculated?.Invoke(_currentCompletePercent);
        }

        private void Initialize()
        {
            foreach (var skinConfig in _skinConfigs)
            {
                if (skinConfig.ID == _currentOpenableSkinID)
                {
                    Initialized?.Invoke(skinConfig.Icon, _currentCompletePercent);
                    break;
                }
            }
        }
    }
}
