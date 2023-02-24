using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ItemView _template;
    [SerializeField] private Transform _content;
    [SerializeField] private ShopScroll _shopScroll;
    [SerializeField] private List<Item> _items;

    private void Awake()
    {
        List<ItemView> spawnedItems = new List<ItemView>();

        foreach (var item in _items)
        {
            var spawnedItem = Instantiate(_template, _content);

            spawnedItem.Initialize(item);
            spawnedItems.Add(spawnedItem);
        }

        _shopScroll.Initialize(spawnedItems);
    }
}
