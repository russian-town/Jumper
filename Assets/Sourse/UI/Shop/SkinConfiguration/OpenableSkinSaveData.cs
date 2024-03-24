using System;

namespace Sourse.UI.Shop.SkinConfiguration
{
    [Serializable]
    public class OpenableSkinSaveData : SkinSaveData
    {
        public OpenableSkinSaveData(bool isBought, bool isSelect, float persent) : base(isBought, isSelect)
            => Persent = persent;

        public float Persent { get; private set; }
    }
}
