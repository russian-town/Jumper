using System.Collections.Generic;
using Sourse.Constants;
using Sourse.Finish;
using Sourse.Player.Common.Scripts;
using Sourse.Save;
using Sourse.UI;
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
        private readonly OpenableSkinBar _openableSkinBar;

        private float _currentCompletePercent;
        private int _currentOpenableSkinID = -1;
        private List<OpenableSkinSaveData> _aviableOpenableSkinSaveData = new();

        public OpenableSkinViewFiller(LevelCompletePanel levelCompletePanel,
            List<SkinConfig> skinConfigs,
            GroundDetector groundDetector,
            OpenableSkinBar openableSkinBar)
        {
            _levelCompletePanel = levelCompletePanel;
            _skinConfigs = skinConfigs;
            _groundDetector = groundDetector;
            _openableSkinBar = openableSkinBar;
        }

        public void Subscribe()
            => _groundDetector.Fell += OnFell;

        public void Unsubscribe()
            => _groundDetector.Fell -= OnFell;

        public void Read(PlayerData playerData)
        {
            foreach (var openableSkinSaveData in playerData.OpenableSkinSaveDatas)
            {
                if (openableSkinSaveData.Persent < PlayerParameter.MaxPercent)
                {
                    _aviableOpenableSkinSaveData.Add(openableSkinSaveData);
                    Debug.Log(openableSkinSaveData.ID);
                }
            }

            if (_aviableOpenableSkinSaveData.Count == 0)
            {
                Debug.Log("all skin opened");
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
                        Debug.Log($"current {openableSkinSaveData.ID} opened on {_currentCompletePercent} percent");
                        break;
                    }
                }

                InitializeOpenableSkinBar();
                return;
            }

            int index = Random.Range(PlayerParameter.MinIndex, _aviableOpenableSkinSaveData.Count);
            _currentOpenableSkinID = _aviableOpenableSkinSaveData[index].ID;
            Debug.Log($"Random ID = {_currentOpenableSkinID}");
            InitializeOpenableSkinBar();
        }

        public void Write(PlayerData playerData)
        {
            Debug.Log("Data writed!");
           
            foreach (var openableSkinSaveData in playerData.OpenableSkinSaveDatas)
            {
                if (openableSkinSaveData.ID == _currentOpenableSkinID)
                {
                    openableSkinSaveData.Persent = _currentCompletePercent;
                    Debug.Log($"skin ID = {_currentOpenableSkinID} writed {_currentCompletePercent} percent");
                    break;
                }
            }

            if (_currentCompletePercent >= PlayerParameter.MaxPercent)
            {
                _currentCompletePercent = PlayerParameter.MaxPercent;
                Debug.Log($"Current ID {_currentOpenableSkinID} opened!");
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
                    Debug.Log($"Openable skin bar initialized");
                    _openableSkinBar.Initialize(skinConfig.Icon, _currentCompletePercent);
                    break;
                }
            }
        }

        private void OnFell(Collision collision)
        {
            if (collision.transform.TryGetComponent(out LevelCompleteSoundPlayer _))
                CalculatePercent();
        }

        private void CalculatePercent()
        {
            _currentCompletePercent += PlayerParameter.MaxPercent;
            _levelCompletePanel.Show();
            _levelCompletePanel.SetText(1);

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
