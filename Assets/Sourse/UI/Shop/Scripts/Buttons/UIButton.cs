using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class UIButton : UIElement
{
    public event UnityAction ButtonClicked;

    private Button _button;
    private bool _isInitialized;

    private void OnDisable()
    {
        if (_isInitialized == false)
            return;

        _button.onClick.RemoveListener(ButtonClick);
    }

    public void Initialize()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ButtonClick);
        _isInitialized = true;
    }

    protected void SwitchEnableState(bool isEnable)
    {
        _button.enabled = isEnable;
    }

    private void ButtonClick()
    {
        ButtonClicked?.Invoke();
    }
}
