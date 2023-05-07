using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SkinViewSpawner))]
public class Shop : SkinHandler
{
    public event UnityAction<int> Selected;
    public event UnityAction<int> Initialized;

    [SerializeField] private Transform _content;
    [SerializeField] private ShopScroll _shopScroll;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Skin _defaultSkin;

    private SkinViewSpawner _skinViewSpawner;
    private List<SkinView> _spawnedSkinsView = new List<SkinView>();
    private SkinView _currentSkinView;
    private Skin _currentSkin;
    private int _selectedID;

    public int SelectedID => _selectedID;

    private void Awake()
    {
        _skinViewSpawner = GetComponent<SkinViewSpawner>();
        Initialize();

        for (int i = 0; i < Skins.Count; i++)
        {
            var spawnedSkinView = _skinViewSpawner.GetSkinView(Skins[i]);
            spawnedSkinView.Selected += OnSkinViewSelected;
            spawnedSkinView.ByButtonClicked += OnByButtonClicked;
            spawnedSkinView.SelectButtonClicked += OnSelectButtonClicked;
            spawnedSkinView.Initialize(Skins[i]);
            _spawnedSkinsView.Add(spawnedSkinView);
        }

        if (_currentSkin == null)
        {
            _currentSkin = _defaultSkin;
            _defaultSkin.Select();
            _currentSkinView = _skinViewSpawner.DefaultSkin;
            _currentSkinView.UpdateView();
            _selectedID = _defaultSkin.ID;
        }

        _shopScroll.Initialize(_spawnedSkinsView);
        Initialized?.Invoke(_selectedID);
    }

    private void OnDisable()
    {
        foreach (var skinView in _spawnedSkinsView)
        {
            skinView.Selected -= OnSkinViewSelected;
            skinView.ByButtonClicked -= OnByButtonClicked;
            skinView.SelectButtonClicked -= OnSelectButtonClicked;
        }
    }

    public void OpenSkin(int id) => TryBySkin(id);

    public bool TryBySkin(int id)
    {
        if (TrySearchByID(id, out Skin skin) == true)
        {
            if (skin.Price <= _wallet.Money)
            {
                _wallet.DicreaseMoney(skin.Price);
                skin.By();
                Saver.SaveState(IsByKey, skin.ID, skin.IsBy);
                return true;
            }
        }

        return false;
    }

    public bool TrySelect(int id)
    {
        if (TrySearchByID(id, out Skin skin))
        {
            if (skin.IsBy == true)
            {
                DeselectSkin(_selectedID);
                skin.Select();
                Saver.SaveState(IsSelectKey, skin.ID, skin.IsSelect);
                _selectedID = id;
                Saver.SaveSelectedID(_selectedID);
                Selected?.Invoke(id);
                return true;
            }
        }

        return false;
    }

    private bool TrySearchByID(int id, out Skin skin)
    {
        for (int i = 0; i < Skins.Count; i++)
        {
            if (Skins[i].ID == id)
            {
                skin = Skins[i];
                return true;
            }
        }

        skin = null;
        return false;
    }

    private void OnSkinViewSelected(Skin skin, SkinView skinView)
    {
        _currentSkinView = skinView;
        _currentSkin = skin;
        _selectedID = skin.ID;
    }

    private void OnByButtonClicked(Skin skin, SkinView skinView)
    {
        if (TryBySkin(skin.ID))
            skinView.UpdateView();
    }

    private void OnSelectButtonClicked(Skin skin, SkinView skinView)
    {
        if (TrySelect(skin.ID))
        {
            if (_currentSkinView != null)
                _currentSkinView.UpdateView();

            skinView.UpdateView();
            _currentSkinView = skinView;
        }
    }

    private void DeselectSkin(int id)
    {
        if (TrySearchByID(id, out Skin skin))
        {
            skin.Deselect();
            Saver.SaveState(IsSelectKey, skin.ID, skin.IsSelect);
        }
    }
}
