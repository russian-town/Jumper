using Sourse.UI.Shop.View.OpenableView;
using UnityEngine;

namespace Sourse.UI.Shop.View.Common.OpenableView
{
    public class OpenableSkinView : SkinView
    {
        [SerializeField] private LockImage _lockImage;

        private Color _openSkinColor = Color.white;
        private Color _closeSkinColor = Color.black;

        protected override void UpdateChildView()
        {
            //if (Skin.IsBought == false)
            //{
            //    SetIconColor(_closeSkinColor);
            //    SelectButton.Hide();
            //}
            //else
            //{
            //    SetIconColor(_openSkinColor);
            //    SelectButton.Show();
            //    _lockImage.Hide();
            //}
        }

        protected override void Initialize() { }

        protected override void Deinitialize() { }
    }
}
