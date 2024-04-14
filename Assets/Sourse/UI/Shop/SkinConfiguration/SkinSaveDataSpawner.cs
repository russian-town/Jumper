namespace Sourse.UI.Shop.SkinConfiguration
{
    public class SkinSaveDataSpawner
    {
        public SkinSaveData CreateSkinSaveData(Skin skin)
        {
            SkinSaveData skinSaveData = new ();
            skinSaveData.ID = skin.ID;
            skinSaveData.IsBought = skin.IsBought;
            skinSaveData.IsSelect = skin.IsSelect;
            return skinSaveData;
        }

        public OpenableSkinSaveData CreateOpenableSkinSaveData(OpenableSkin openableSkin)
        {
            OpenableSkinSaveData openableSkinSaveData = new ();
            openableSkinSaveData.ID = openableSkin.ID;
            openableSkinSaveData.IsBought = openableSkin.IsBought;
            openableSkinSaveData.IsSelect = openableSkin.IsSelect;
            openableSkinSaveData.Persent = openableSkin.Percent;
            return openableSkinSaveData;
        }
    }
}
