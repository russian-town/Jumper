using UnityEngine;

namespace Sourse.UI.Shop.SkinConfiguration
{
    public class SkinViewSpawner
    {
        public SkinView Get(SkinView template, Transform parent)
        {
            return Object.Instantiate(template, parent);
        }
    }
}
