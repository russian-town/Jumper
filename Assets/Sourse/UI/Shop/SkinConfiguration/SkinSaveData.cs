namespace Sourse.UI.Shop.SkinConfiguration
{
    [System.Serializable]
    public abstract class SkinSaveData
    {
        public SkinSaveData(bool isBought, bool isSelect)
        {
            IsBought = isBought;
            IsSelect = isSelect;
        }

        public bool IsBought { get; private set; }
        public bool IsSelect { get; private set; }
        public int Id { get; private set; }
    }
}   
