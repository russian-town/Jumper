using Sourse.UI.Shop.View.Common;

namespace Sourse.UI.Shop.SkinConfiguration
{
    public class SkinSpawner
    {
        public Skin Get(SkinView skinView, SkinConfig skinConfig)
        {
            return new Skin(skinView, skinConfig);
        }
    }
}
