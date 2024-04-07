using System;
using Sourse.Constants;
using Sourse.UI;
using Sourse.UI.Shop.Scripts.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Sourse.Settings.Audio
{
    public class AudioView : UIElement
    {
        [SerializeField] private Image _soundImage;
        [SerializeField] private Image _musicImage;
        [SerializeField] private Sprite _unmuteSound;
        [SerializeField] private Sprite _muteSound;
        [SerializeField] private Sprite _unmuteMusic;
        [SerializeField] private Sprite _muteMusic;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private CloseButton _closeButton;

        private Audio _audio;

        public event Action Closed;

        public void Subscribe()
        {
            _soundSlider.onValueChanged.AddListener(_audio.ChangeSoundVolume);
            _soundSlider.onValueChanged.AddListener(OnSoundSliderValueChaged);
            _musicSlider.onValueChanged.AddListener(_audio.ChangeMusicVolume);
            _musicSlider.onValueChanged.AddListener(OnMusicSliderValueChaged);
            _audio.SoundValueChanged += OnSoundValueChanged;
            _audio.MusicValueChanged += OnMusicValueChanged;
            _closeButton.AddListener(Hide);
            _closeButton.AddListener(() => Closed?.Invoke());
        }

        public void Unsubscribe()
        {
            _soundSlider.onValueChanged.RemoveListener(_audio.ChangeSoundVolume);
            _soundSlider.onValueChanged.RemoveListener(OnSoundSliderValueChaged);
            _musicSlider.onValueChanged.RemoveListener(_audio.ChangeMusicVolume);
            _musicSlider.onValueChanged.RemoveListener(OnMusicSliderValueChaged);
            _audio.SoundValueChanged -= OnSoundValueChanged;
            _audio.MusicValueChanged -= OnMusicValueChanged;
            _closeButton.RemoveListener(Hide);
            _closeButton.RemoveListener(() => Closed?.Invoke());
        }

        public void Initialize(Audio audio)
        {
            _audio = audio;
            _closeButton.Initialize();
            Hide();
        }

        private void OnSoundValueChanged(float value)
            => _soundSlider.value = value;

        private void OnMusicValueChanged(float value)
            => _musicSlider.value = value;

        private void OnSoundSliderValueChaged(float value)
        {
            if (value <= AudioParameters.MuteVolume)
                _soundImage.sprite = _muteSound;
            else
                _soundImage.sprite = _unmuteSound;
        }

        private void OnMusicSliderValueChaged(float value) 
        {
            if (value <= AudioParameters.MuteVolume)
                _musicImage.sprite = _muteMusic;
            else
                _musicImage.sprite = _unmuteMusic;
        }
    }
}
