using System.Collections.Generic;
using Sourse.UI.Shop.SkinConfiguration;

namespace Sourse.Save
{
    public class SaveDataInjector
    {
        private List<SkinSaveData> _skinSaveDatas;

        public SaveDataInjector(List<SkinSaveData> skinSaveDatas)
            => _skinSaveDatas = skinSaveDatas;

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

        public void Write(Skin skin)
        {
            if (_skinSaveDatas == null)
            {
                _skinSaveDatas = new List<SkinSaveData>();
                AddSkinSaveData(skin);
                return;
            }

            if(_skinSaveDatas.Count == 0)
            {
                AddSkinSaveData(skin);
                return;
            }

            if (TrySearchSkinSaveData(out SkinSaveData searchData, skin))
                searchData.ApplyData(skin);
            else
                AddSkinSaveData(skin);
        }

        private void AddSkinSaveData(Skin skin)
        {
            var newSkinSaveData = new SkinSaveData();
            newSkinSaveData.ApplyData(skin);
            _skinSaveDatas.Add(newSkinSaveData);
        }

        private bool TrySearchSkinSaveData(out SkinSaveData searchData, Skin skin)
        {
            foreach (var skinSaveData in _skinSaveDatas)
            {
                if (skinSaveData.ID == skin.ID)
                {
                    searchData = skinSaveData;
                    return true;
                    break;
                }
            }

            searchData = null;
            return false;
        }
    }
}
