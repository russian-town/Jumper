using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;
    [SerializeField] private Image _boughtIcon;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Button _button;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Color _boughtButtonColor;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void Initialize(Item item)
    {
        _itemIcon.sprite = item.Icon;
        _priceText.text = item.Price.ToString();
        _boughtIcon.enabled = false;
    }

    private void OnButtonClick()
    {
        _priceText.enabled = false;
        _boughtIcon.enabled = true;
        _buttonImage.color = _boughtButtonColor;
    }
}
