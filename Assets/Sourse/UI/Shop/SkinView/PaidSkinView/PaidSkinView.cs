using UnityEngine;

public class PaidSkinView : SkinView
{
    [SerializeField] private ByButton _byButton;
    [SerializeField] private PriceText _priceText;

    protected override void UpdateChildView()
    {
        if (Skin.IsBy == true)
        {
            _byButton.Hide();
            SelectButton.Show();
        }
        else if (Skin.IsBy == false)
        {
            _priceText.SetText(Skin.Price);
            _byButton.Show();
        }
    }

    protected override void Subscribe()
    {
        _byButton.ButtonClicked += OnButtonClicked;
    }

    protected override void Deinitialize()
    {
        _byButton.ButtonClicked -= OnButtonClicked;
    }

    private void OnButtonClicked()
    {
        By();
    }
}
