using Sourse.Constants;

namespace Sourse.UI.Shop.SkinConfiguration
{
    public class OpenableSkin : Skin
    {
        public OpenableSkin(SkinConfig skinConfig) : base(skinConfig)
        {
        }

        public float Percent { get; private set; }

        public void ApplySaveData(OpenableSkinSaveData openableSkinSaveData)
        {
            base.ApplySaveData(openableSkinSaveData);
            Percent = openableSkinSaveData.Persent;

            if (Percent >= PlayerParameter.MaxPercent)
            {
                Percent = PlayerParameter.MaxPercent;
                Buy();
            }
        }
    }
}
