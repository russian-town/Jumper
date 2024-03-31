namespace Sourse.UI.Shop.SkinConfiguration
{
    [System.Serializable]
    public class SkinSaveData
    {
        public bool IsBought; 
        public bool IsSelect;
        public int ID = -1;

        public void ApplyData(Skin skin)
        {
            IsBought = skin.IsBought;
            IsSelect = skin.IsSelect;

            if (ID < 0)
                ID = skin.ID;
        }
    }
}   
