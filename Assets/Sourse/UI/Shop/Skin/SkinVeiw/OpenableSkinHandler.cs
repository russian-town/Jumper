using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OpenableSkinIDSaver))]
public class OpenableSkinHandler : MonoBehaviour
{
    private const string OpeningSkinIndexKey = "OpeningSkinIndex";

    private OpenableSkinIDSaver _openableSkinIDSaver;
    private List<Skin> _openableSkins = new List<Skin>();
    private Shop _shop;
    private LevelCompletePanel _levelCompletePanel;

    private void OnDisable()
    {
        _levelCompletePanel.SkinOpened -= OnSkinOpened;
    }

    public void Initialize(Shop shop, LevelCompletePanel levelCompletePanel)
    {
        _openableSkinIDSaver = GetComponent<OpenableSkinIDSaver>();
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

        if (_openableSkinIDSaver.TryGetValue(OpeningSkinIndexKey, out int value))
        {
            index = value;
        }
        else
        {
            index = Random.Range(0, _openableSkins.Count);
            _openableSkinIDSaver.Save(OpeningSkinIndexKey, index);
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
                _openableSkinIDSaver.TryDeleteSaveData(OpeningSkinIndexKey);
                i--;
            }
        }
    }
}
