using Sourse.UI.Shop.View.Common;

namespace Sourse.UI.Shop.SkinConfiguration
{
    public class Skin
    {
        private SkinView _skinView;
        private SkinConfig _skinConfig;
        private bool _isSelect;

        public Skin(SkinView skinView, SkinConfig skinConfig)
        {
            _skinView = skinView;
            _skinConfig = skinConfig;
            ID = _skinConfig.ID;
            Price = skinConfig.Price;
        }

        public bool IsBought {  get; private set; }
        public int Price { get; private set; }
        public int ID { get; private set; }

        public void ApplySaveData(SkinSaveData skinSaveData)
        {
            if (skinSaveData.IsSelect)
                Select();

            if (skinSaveData.IsBought)
                Buy();
        }

        public void Select()
            => _isSelect = true;

        public void Buy()
            => IsBought = true;
    }
}
