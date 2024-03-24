using System.Collections.Generic;
using Sourse.UI.Shop.SkinConfiguration;

namespace Sourse.UI.Shop.Scripts
{
    public class BubbleSort
    {
        public void SortingSkins(ref List<SkinConfig> skinConfigs)
        {
            for (int i = 0; i < skinConfigs.Count; i++)
            {
                for (int j = i + 1; j < skinConfigs.Count; j++)
                {
                    /*if (skins[i].SortOrder > skins[j].SortOrder)
                    {
                        var skin = skins[i];
                        skins[i] = skins[j];
                        skins[j] = skin;
                    }*/
                }
            }
        }
    }
}
