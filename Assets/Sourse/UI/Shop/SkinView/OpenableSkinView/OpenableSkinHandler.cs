using System.Collections.Generic;
using Sourse.UI.LevelCompletePanel;
using UnityEngine;

public class OpenableSkinHandler : SkinHandler
{
    private const string OpeningSkinIndexKey = "OpeningSkinIndex";

    private List<Skin> _openableSkins = new List<Skin>();
    private LevelCompletePanel _levelCompletePanel;
    private int _currentIndex;

    private void OnDisable() => _levelCompletePanel.SkinOpened -= OnSkinOpened;

    public void Initialize(LevelCompletePanel levelCompletePanel)
    {
        Initialize();
        _levelCompletePanel = levelCompletePanel;
        _levelCompletePanel.SkinOpened += OnSkinOpened;

        foreach (var skin in Skins)
            AddOpenableSkin(skin);
    }

    public Skin GetOpenableSkin()
    {
        if (_openableSkins.Count <= 0)
            return null;

        if (Saver.TryGetValue(OpeningSkinIndexKey, out int value))
        {
            _currentIndex = value;
        }
        else
        {
            _currentIndex = Random.Range(0, _openableSkins.Count);
            Saver.Save(OpeningSkinIndexKey, _currentIndex);
        }

        return _openableSkins[_currentIndex];
    }

    private void AddOpenableSkin(Skin skin)
    {
        if (skin.Type == SkinType.Openable && skin.IsBy == false)
            _openableSkins.Add(skin);
    }

    private void OnSkinOpened(int id)
    {
        OpenSkin(id);

        for (int i = 0; i < _openableSkins.Count; i++)
        {
            if (_openableSkins[i].ID == id)
            {
                _openableSkins.Remove(_openableSkins[i]);
                Saver.TryDeleteSaveData(OpeningSkinIndexKey);
                i--;
            }
        }
    }

    private void OpenSkin(int id)
    {
        foreach (var skin in Skins)
        {
            if (id == skin.ID)
            {
                skin.By();
                Saver.SaveState(IsByKey, id, skin.IsBy);
            }
        }
    }
}
