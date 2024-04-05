using Sourse.Constants;
using Sourse.Save;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Sourse.Settings.Audio
{
    public class Audio : IDataReader, IDataWriter
    {
        private readonly AudioMixerGroup _soundGroup;
        private readonly AudioMixerGroup _musicGroup;

        private float _soundValue;
        private float _musicValue;

        public event Action<float> SoundValueChanged;
        public event Action<float> MusicValueChanged;

        public Audio(AudioMixerGroup soundGroup,
            AudioMixerGroup musicGroup)
        {
            _soundGroup = soundGroup;
            _musicGroup = musicGroup;
        }

        public void Read(PlayerData playerData)
        {
            _soundValue = playerData.SoundValue;
            _musicValue = playerData.MusicValue;
            SoundValueChanged?.Invoke(_soundValue);
            MusicValueChanged?.Invoke(_musicValue);
        }

        public void Write(PlayerData playerData)
        {
            playerData.SoundValue = _soundValue;
            playerData.MusicValue = _musicValue;
            Debug.Log(playerData.SoundValue);
        }

        public void Mute()
            => AudioListener.pause = true;

        public void Unmute()
            => AudioListener.pause = false;

        public void ChangeSoundVolume(float value)
        {
            _soundValue = value;
            float soundVolume = Mathf.Log10(_soundValue) * AudioParameters.AudioMixerValueRatio;
            _soundGroup.audioMixer.SetFloat(AudioParameters.MusicGroup, soundVolume);
        }

        public void ChangeMusicVolume(float value)
        {
            _musicValue = value;
            float musicVolume = Mathf.Log10(_musicValue) * AudioParameters.AudioMixerValueRatio;
            _musicGroup.audioMixer.SetFloat(AudioParameters.MusicGroup, musicVolume);
        }
    }
}
