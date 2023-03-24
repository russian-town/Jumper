using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class PriceText : UIElement
{
    private TMP_Text _text;

    public void SetText(int price)
    {
        _text = GetComponent<TMP_Text>();
        _text.text = price.ToString();
    }
}
