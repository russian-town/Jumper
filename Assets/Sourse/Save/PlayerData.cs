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
        public int SavedPropsIndex = -1;
        public int CurrentOpenableSkinID = -1;
        public float SoundValue = 1f;
        public float MusicValue = 1f;
    }
}
