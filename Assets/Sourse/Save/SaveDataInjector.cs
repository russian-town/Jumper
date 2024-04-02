using System.Collections.Generic;
using Sourse.UI.Shop.SkinConfiguration;

namespace Sourse.Save
{
    public class SaveDataInjector
    {
        private readonly List<SkinSaveData> _skinSaveDatas;
        private readonly List<OpenableSkinSaveData> _openableSkinSaveDatas;

        public SaveDataInjector(List<SkinSaveData> skinSaveDatas,
            List<OpenableSkinSaveData> openableSkinSaveDatas)
        {
            _skinSaveDatas = skinSaveDatas;
            _openableSkinSaveDatas = openableSkinSaveDatas;
        }

        public void Update(Skin skin)
        {
            if (_skinSaveDatas == null)
                return;

            foreach (var skinSaveData in _skinSaveDatas)
            {
                if(skinSaveData.ID == skin.ID)
                {
                    skin.ApplySaveData(skinSaveData);
                    break;
                }
            }
        }

        public void Update(OpenableSkin openableSkin)
        {
            if (_openableSkinSaveDatas == null)
                return;

            foreach (var openableSkinSaveData in _openableSkinSaveDatas)
            {
                if (openableSkinSaveData.ID == openableSkin.ID)
                {
                    openableSkin.ApplySaveData(openableSkinSaveData);
                    break;
                }
            }
        }

        public void Write(Skin skin)
        {
            foreach (var skinSaveData in _skinSaveDatas)
            {
                if(skinSaveData.ID == skin.ID)
                {
                    skinSaveData.IsBought = skin.IsBought;
                    skinSaveData.IsSelect = skin.IsSelect;
                    break;
                }
            }
        }

        public void Write(OpenableSkin openableSkin)
        {
            foreach (var openableSkinSaveData in _openableSkinSaveDatas)
            {
                if(openableSkinSaveData.ID == openableSkin.ID)
                {
                    openableSkinSaveData.IsBought = openableSkin.IsBought;
                    openableSkinSaveData.IsSelect = openableSkin.IsSelect;
                    openableSkinSaveData.Persent = openableSkin.Percent;
                    break;
                }
            }
        }
    }
}
