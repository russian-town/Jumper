using System.Collections.Generic;
using Sourse.UI.Shop.SkinConfiguration;

namespace Sourse.UI.Shop.Scripts
{
    public class BubbleSort
    {
        public void SortingSkins(ref List<SkinConfig> configs)
        {
            for (int i = 0; i < configs.Count; i++)
            {
                for (int j = i + 1; j < configs.Count; j++)
                {
                    if (configs[i].ID > configs[j].ID)
                    {
                        SkinConfig skin = configs[i];
                        configs[i] = configs[j];
                        configs[j] = skin;
                    }
                }
            }
        }
    }
}
