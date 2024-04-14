using UnityEngine;

namespace Sourse.UI.Shop.SkinConfiguration
{
    [CreateAssetMenu(fileName = "NewSkinConfig", menuName = "Skin Config/New Skin Config", order = 59)]
    public class SkinConfig : ScriptableObject
    {
        [field: SerializeField] public SkinType Type { get; private set; }

        [field: SerializeField] public Sprite Icon { get; private set; }

        [field: SerializeField] public int Price { get; private set; }

        [field: SerializeField] public int ID { get; private set; }
    }
}
