using UnityEngine;
using UnityEngine.UI;

public class AudioView : MonoBehaviour
{
    [SerializeField] private Image _soundImage;
    [SerializeField] private Image _musicImage;
    [SerializeField] private Sprite _unmuteSound;
    [SerializeField] private Sprite _muteSound;
    [SerializeField] private Sprite _unmuteMusic;
    [SerializeField] private Sprite _muteMusic;

    public void MuteSound()
    {
        ChangeIcon(_soundImage, _muteSound);
    }

    public void UnmuteSound()
    {
        ChangeIcon(_soundImage, _unmuteSound);
    }

    public void MuteMusic()
    {
        ChangeIcon(_musicImage, _muteMusic);
    }

    public void UnmuteMusic()
    {
        ChangeIcon(_musicImage, _unmuteMusic);
    }

    private void ChangeIcon(Image icon, Sprite iconState)
    {
        icon.sprite = iconState;
    }
}
