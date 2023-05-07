using UnityEngine;

public class Menu : UIElement
{
    [SerializeField] private SettingButton _settingButton;
    [SerializeField] private CloseButton _closeSettingButton;
    [SerializeField] private Pause _pause;

    private void OnEnable()
    {
        _settingButton.ButtonClicked += OnSettingButtonClicked;
        _closeSettingButton.ButtonClicked += OnClouseSettingButtonClicked;
    }

    private void OnDisable()
    {
        _settingButton.ButtonClicked -= OnSettingButtonClicked;
        _closeSettingButton.ButtonClicked -= OnClouseSettingButtonClicked;
    }

    private void OnSettingButtonClicked()
    {
        _pause.Enable();
    }

    private void OnShopButtonClicked()
    {
        _pause.Enable();
    }

    private void OnClouseShopeButtonClicked()
    {
        _pause.Disable();
    }

    private void OnClouseSettingButtonClicked()
    {
        _pause.Disable();
    }
}
