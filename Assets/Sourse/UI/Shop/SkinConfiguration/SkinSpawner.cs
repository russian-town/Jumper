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
