using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SkinViewSpawner), typeof(Sorter), typeof(Saver))]
public class Shop : MonoBehaviour
{
    public event UnityAction<int> Selected;
    public event UnityAction<int> Initialized;

    private const string SelectedIDKey = "SelectedIDKey";
    //Переделать чушь

    [SerializeField] private bool _isDelete;
    [SerializeField] private Transform _content;
    [SerializeField] private ShopScroll _shopScroll;
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private Wallet _wallet;

    private SkinViewSpawner _skinViewSpawner;
    private Sorter _sorter;
    private List<SkinView> _spawnedSkinsView = new List<SkinView>();
    private Saver _saver;
    private int _selectedID;

    public int SelectedID => _selectedID;

    private void OnValidate()
    {
        if(_isDelete == true)
        {
            PlayerPrefs.DeleteAll();
            _isDelete = false;
        }
    }

    public void Initialize(OpenableSkinHandler openableSkinHandler, Skin defautSkin)
    {
        _skinViewSpawner = GetComponent<SkinViewSpawner>();
        _sorter = GetComponent<Sorter>();
        _saver = GetComponent<Saver>();

        if (_saver.TryGetValue(SelectedIDKey, out int value))
        {
            _selectedID = value;

            if (TrySearchByID(value, out Skin skin))
                skin.Select();
        }
        else
        {
            defautSkin.Select();
            _selectedID = defautSkin.ID;
        }

        List<Skin> sortingSkins = _sorter.SortingSkins(_skins);

        for (int i = 0; i < sortingSkins.Count; i++)
        {
            if (_saver.TryGetValue(sortingSkins[i].ID))
                sortingSkins[i].By();

            openableSkinHandler.AddOpenableSkin(sortingSkins[i]);
            var spawnedSkinView = _skinViewSpawner.GetSkinView(sortingSkins[i]);
            spawnedSkinView.Initialize(sortingSkins[i], this);
            _spawnedSkinsView.Add(spawnedSkinView);
        }

        _shopScroll.Initialize(_spawnedSkinsView);

        Initialized?.Invoke(_selectedID);
    }

    public void OpenSkin(int id)
    {
        TryBySkin(id);
    }

    public bool TryBySkin(int id)
    {
        if (TrySearchByID(id, out Skin skin) == true)
        {
            if (skin.Price <= _wallet.Money)
            {
                _wallet.DicreaseMoney(skin.Price);
                skin.By();
                _saver.Save(skin.ID.ToString(), 1);
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
                DeselectSkins();
                skin.Select();
                _selectedID = id;
                _saver.TryDeleteSaveData(SelectedIDKey);
                _saver.Save(SelectedIDKey, id);
                Selected?.Invoke(id);
                return true;
            }
        }

        return false;
    }

    private bool TrySearchByID(int id, out Skin skin)
    {
        for (int i = 0; i < _skins.Count; i++)
        {
            if (_skins[i].ID == id)
            {
                skin = _skins[i];
                return true;
            }
        }

        skin = null;
        return false;
    }

    private void DeselectSkins()
    {
        foreach (var spawnedSkinsView in _spawnedSkinsView)
        {
            spawnedSkinsView.Deselect();
        }
    }
}
