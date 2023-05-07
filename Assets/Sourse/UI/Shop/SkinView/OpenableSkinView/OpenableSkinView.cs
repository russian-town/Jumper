using UnityEngine;

public class OpenableSkinView : SkinView
{
    private Color _openSkinColor = Color.white;
    private Color _closeSkinColor = Color.black;

    protected override void UpdateChildView()
    {
        if (Skin.IsBy == false)
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

    protected override void Subscribe() {}

    protected override void Deinitialize() {}
}
