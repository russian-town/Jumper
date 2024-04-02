using UnityEngine;

namespace Sourse.UI.Shop.SkinConfiguration
{
    public class SkinViewSpawner
    {
        public PaidSkinView Get(PaidSkinView template, Transform parent)
        {
            return Object.Instantiate(template, parent);
        }
    }
}
