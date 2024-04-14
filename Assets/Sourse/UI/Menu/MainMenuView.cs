using System;
using Sourse.Settings.Audio;
using Sourse.UI.Shop.Scripts.Buttons;
using UnityEngine;

namespace Sourse.Menu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private SettingsButton _settingsButton;
        [SerializeField] private ShopButton _shopButton;
        [SerializeField] private PlayButton _playButton;
        [SerializeField] private AudioView _audioView;

        public event Action ShopButtonClicked;

        public event Action PlayButtonClicked;

        public void Initialize()
        {
            _settingsButton.Initialize();
            _playButton.Initialize();
            _shopButton.Initialize();
        }

        public void Subscribe()
        {
            _settingsButton.AddListener(_audioView.Show);
            _shopButton.AddListener(() => ShopButtonClicked?.Invoke());
            _playButton.AddListener(() => PlayButtonClicked?.Invoke());
        }

        public void Unsubscribe()
        {
            _settingsButton.RemoveListener(_audioView.Show);
            _shopButton.RemoveListener(() => ShopButtonClicked?.Invoke());
            _playButton.RemoveListener(() => PlayButtonClicked?.Invoke());
        }
    }
}
