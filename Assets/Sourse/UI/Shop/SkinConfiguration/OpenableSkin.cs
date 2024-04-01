namespace Sourse.UI.Shop.SkinConfiguration
{
    public class OpenableSkin : Skin
    {
        public OpenableSkin(SkinConfig skinConfig) : base(skinConfig)
        {
        }

        public float Percent { get; private set; }

        public void ApplyData(OpenableSkinSaveData saveData)
        {
            ApplySaveData(saveData);
            Percent = saveData.Persent;
        }
    }
}
