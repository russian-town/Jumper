using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 66)]
public class Item : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
}
