using UnityEngine;

public class PaidSkinView : SkinView
{
    [SerializeField] private ByButton _byButton;
    [SerializeField] private PriceText _priceText;

    private void Start()
    {
        if (Skin.IsBy == true)
        {
            _byButton.Hide();
            SelectButton.Show();
        }
    }
    private void OnEnable()
    {
        _byButton.ButtonClicked += OnButtonClicked;
    }

    private void OnDisable()
    {
        _byButton.ButtonClicked -= OnButtonClicked;
    }

    public override void Initialize(Skin skin, Shop shop)
    {
        base.Initialize(skin, shop);

        if (skin.IsBy == false)
        {
            _priceText.SetText(skin.Price);
            _byButton.Show();
        }
    }

    private void OnButtonClicked()
    {
        base.By();
    }
}
