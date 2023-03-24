using UnityEngine;

public class Menu : UIElement
{
    [SerializeField] private SettingButton _settingButton;
    [SerializeField] private ShopButton _shopButton;
    [SerializeField] private CloseButton _closeShopButton;
    [SerializeField] private CloseButton _closeSettingButton;
    [SerializeField] private Pause _pause;

    private void OnEnable()
    {
        _settingButton.ButtonClicked += OnSettingButtonClicked;
        _shopButton.ButtonClicked += OnShopButtonClicked;
        _closeSettingButton.ButtonClicked += OnClouseSettingButtonClicked;
        _closeShopButton.ButtonClicked += OnClouseShopeButtonClicked;
    }

    private void OnDisable()
    {
        _settingButton.ButtonClicked -= OnSettingButtonClicked;
        _shopButton.ButtonClicked -= OnShopButtonClicked;
        _closeSettingButton.ButtonClicked -= OnClouseSettingButtonClicked;
        _closeShopButton.ButtonClicked -= OnClouseShopeButtonClicked;
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
