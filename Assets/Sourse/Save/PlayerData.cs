using System.Collections.Generic;
using Sourse.UI.Shop.SkinConfiguration;

namespace Sourse.Save
{
    [System.Serializable]
    public class PlayerData
    {
        public List<SkinSaveData> SkinSaveDatas;
        public List<OpenableSkinSaveData> OpenableSkinSaveDatas;
        public bool IsTutorialCompleted;
        public int Money;
        public int LastPropsIndex = -1;
    }
}
