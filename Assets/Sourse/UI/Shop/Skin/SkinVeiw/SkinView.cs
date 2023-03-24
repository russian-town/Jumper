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

    private Skin _skin;
    private Shop _shop;

    protected Skin Skin => _skin;
    protected Shop Shop => _shop;
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

        if (skin.IsSelect)
            Select();

        if (skin.IsBy)
            By();
    }

    public virtual void By()
    {
        _selectButton.Show();
        SwitchViewState(true, false, _defaultColor);
    }

    public void Select()
    {
        _shop.Select(_skin.ID);
        SwitchViewState(false, true, _selectColor);
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
