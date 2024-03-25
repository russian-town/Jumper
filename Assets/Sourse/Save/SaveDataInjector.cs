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
    }
}
