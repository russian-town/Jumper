using UnityEngine;

namespace Sourse.UI.Shop.SkinConfiguration
{
    public class OpenableSkinView : SkinView
    {
        [SerializeField] private OpenableSkinBar _bar;

        private Skin _openableSkin;

        public void Initialize(OpenableSkin openableSkin)
        {
            Debug.Log(openableSkin.Percent);
            _openableSkin = openableSkin;
            _bar.Initialize(_openableSkin.Icon, openableSkin.Percent);
        }

        protected override Skin Skin()
        {
            return _openableSkin;
        }
    }
}
