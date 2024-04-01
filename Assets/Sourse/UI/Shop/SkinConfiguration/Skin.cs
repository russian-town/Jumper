using System;
using UnityEngine;

namespace Sourse.UI.Shop.SkinConfiguration
{
    public class Skin
    {
        private readonly SkinConfig _skinConfig;
        
        public Skin(SkinConfig skinConfig)
        {
            _skinConfig = skinConfig;
            ID = _skinConfig.ID;
            Price = skinConfig.Price;
            Icon = skinConfig.Icon;
        }

        public bool IsSelect { get; private set; }
        public bool IsBought {  get; private set; }
        public int Price { get; private set; }
        public int ID { get; private set; }
        public Sprite Icon { get; private set; }

        public void ApplySaveData(SkinSaveData skinSaveData)
        {
            if (skinSaveData.IsSelect)
                Select();

            if (skinSaveData.IsBought)
                Buy();
        }

        public void Select()
            =>  IsSelect = true;

        public void Buy()
            => IsBought = true;

        public void Deselect()
            => IsSelect = false;
    }
}
