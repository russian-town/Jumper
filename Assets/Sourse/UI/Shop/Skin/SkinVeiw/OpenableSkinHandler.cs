using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Saver))]
public class OpenableSkinHandler : MonoBehaviour
{
    private const string OpeningSkinIndexKey = "OpeningSkinIndex";

    private Saver _saver;
    private List<Skin> _openableSkins = new List<Skin>();
    private Shop _shop;
    private LevelCompletePanel _levelCompletePanel;

    private void OnDisable()
    {
        _levelCompletePanel.SkinOpened -= OnSkinOpened;
    }

    public void Initialize(Shop shop, LevelCompletePanel levelCompletePanel)
    {
        _saver = GetComponent<Saver>();
        _shop = shop;
        _levelCompletePanel = levelCompletePanel;
        _levelCompletePanel.SkinOpened += OnSkinOpened;
    }

    public void AddOpenableSkin(Skin skin)
    {
        if (skin.Type == SkinType.Openable && skin.IsBy == false)
            _openableSkins.Add(skin);
    }

    public Skin GetOpenableSkin()
    {
        if (_openableSkins.Count <= 0)
            return null;

        int index;

        if (_saver.TryGetValue(OpeningSkinIndexKey, out int value))
        {
            index = value;
        }
        else
        {
            index = Random.Range(0, _openableSkins.Count);
            _saver.Save(OpeningSkinIndexKey, index);
        }

        return _openableSkins[index];
    }

    private void OnSkinOpened(int id)
    {
        _shop.OpenSkin(id);

        for (int i = 0; i < _openableSkins.Count; i++)
        {
            if (_openableSkins[i].ID == id)
            {
                _openableSkins.Remove(_openableSkins[i]);
                _saver.TryDeleteSaveData(OpeningSkinIndexKey);
                i--;
            }
        }
    }
}
