using System;

namespace Sourse.UI.Shop.SkinConfiguration
{
    [Serializable]
    public class PaidSkinSaveData : SkinSaveData
    {
        public PaidSkinSaveData(bool isBought, bool isSelect) : base(isBought, isSelect)
        {
        }
    }
}
