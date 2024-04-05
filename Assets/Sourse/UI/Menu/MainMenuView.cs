using Sourse.Settings.Audio;
using Sourse.UI.Shop.Scripts.Buttons;
using UnityEngine;

namespace Sourse.Menu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private SettingsButton _settingsButton;
        [SerializeField] private AudioView _audioView;

        public void Initialize()
            => _settingsButton.Initialize();

        public void Subscribe()
            => _settingsButton.AddListener(_audioView.Show);

        public void Unsubscribe()
            => _settingsButton.RemoveListener(_audioView.Show);
    }
}
