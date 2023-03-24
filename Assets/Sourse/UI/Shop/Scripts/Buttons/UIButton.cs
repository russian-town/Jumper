using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class UIButton : UIElement
{
    public event UnityAction ButtonClicked;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ButtonClick);
    }

    public void ButtonClick()
    {
        ButtonClicked?.Invoke();
    }
}
