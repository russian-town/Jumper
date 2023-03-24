using UnityEngine;

public class OpenableSkinView : SkinView
{
    private Color _openSkinColor = Color.white;
    private Color _closeSkinColor = Color.black;

    public override void Initialize(Skin skin, Shop shop)
    {
        base.Initialize(skin, shop);

        if (skin.IsBy == false)
        {
            SetIconColor(_closeSkinColor);
            SelectButton.Hide();
        }
        else
        {
            SetIconColor(_openSkinColor);
            SelectButton.Show();
        }
    }
}
