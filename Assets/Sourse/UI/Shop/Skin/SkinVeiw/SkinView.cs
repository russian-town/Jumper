using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SkinView : MonoBehaviour
{
    [SerializeField] private SelectButton _selectButton;
    [SerializeField] private TMP_Text _selectText;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _selectIcon;
    [SerializeField] private Image _selectButtonImage;
    [SerializeField] private Color _selectColor;
    [SerializeField] private Color _defaultColor;

    private Shop _shop;
    private Skin _skin;

    protected Skin Skin => _skin;
    protected SelectButton SelectButton => _selectButton;
    protected TMP_Text SelectText => _selectText;

    private void OnDisable()
    {
        _selectButton.ButtonClicked -= OnButtonClicked;
    }

    public virtual void Initialize(Skin skin, Shop shop)
    {
        _selectButton.ButtonClicked += OnButtonClicked;
        _icon.sprite = skin.Icon;
        _skin = skin;
        _shop = shop;

        if (_skin.IsBy && _skin.IsSelect)
            SwitchViewState(false, true, _selectColor);
        else if (_skin.IsBy && !_skin.IsSelect)
            SwitchViewState(true, false, _defaultColor);
    }

    public virtual void By()
    {
        if (_shop.TryBySkin(_skin.ID))
        {
            _selectButton.Show();
            SwitchViewState(true, false, _defaultColor);
        }
    }

    public void Deselect()
    {
        if (_skin.IsBy == true)
        {
            SwitchViewState(true, false, _defaultColor);
            _skin.Deselect();
        }
    }

    protected void SetIconColor(Color color)
    {
        _icon.color = color;
    }

    private void Select()
    {
        if (_shop.TrySelect(_skin.ID))
        {
            SwitchViewState(false, true, _selectColor);
        }
    }

    private void OnButtonClicked()
    {
        Select();
    }

    private void SwitchViewState(bool enableText, bool enableIcon, Color buttonColor)
    {
        _selectText.enabled = enableText;
        _selectIcon.enabled = enableIcon;
        _selectButtonImage.color = buttonColor;
    }
}
