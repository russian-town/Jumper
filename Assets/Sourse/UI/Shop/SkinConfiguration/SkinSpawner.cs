namespace Sourse.UI.Shop.SkinConfiguration
{
    public class SkinSpawner
    {
        public Skin CreateSkin(SkinConfig skinConfig)
        {
            return new Skin(skinConfig);
        }

        public OpenableSkin CreateOpenableSkin(SkinConfig skinConfig)
        {
            return new OpenableSkin(skinConfig);
        }
    }
}
