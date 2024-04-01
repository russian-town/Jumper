namespace Sourse.UI.Shop.SkinConfiguration
{
    public class SkinSpawner
    {
        public Skin Get(SkinConfig skinConfig)
        {
            return new Skin(skinConfig);
        }
    }
}
