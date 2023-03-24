using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SkinViewSpawner), typeof(Sorter), typeof(SelectedSkinIDSaver))]
[RequireComponent(typeof(SkinStateSaver))]
public class Shop : MonoBehaviour
{
    public event UnityAction<int> Selected;

    private const string SelectedIDKey = "SelectedIDKey";

    [SerializeField] private bool _isDelete;
    [SerializeField] private Transform _content;
    [SerializeField] private ShopScroll _shopScroll;
    [SerializeField] private List<Skin> _skins;
    [SerializeField] private Wallet _wallet;

    private int _selectedID;
    private SkinViewSpawner _skinViewSpawner;
    private Sorter _sorter;
    private List<SkinView> _spawnedSkinsView = new List<SkinView>();
    private SelectedSkinIDSaver _selectedSkinIDSaver;
    private SkinStateSaver _skinStateSaver;

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
        _selectedSkinIDSaver = GetComponent<SelectedSkinIDSaver>();
        _skinStateSaver = GetComponent<SkinStateSaver>();
        _skinViewSpawner = GetComponent<SkinViewSpawner>();
        _sorter = GetComponent<Sorter>();

        if (_selectedSkinIDSaver.TryGetValue(SelectedIDKey, out int value))
            _selectedID = value;
        else
            _selectedID = defautSkin.ID;

        List<Skin> sortingSkins = _sorter.SortingSkins(_skins);

        for (int i = 0; i < sortingSkins.Count; i++)
        {
            if (_skinStateSaver.TryGetValue(sortingSkins[i].ID) == true)
                sortingSkins[i].By();

            if (_selectedSkinIDSaver.TryGetValue(sortingSkins[i].ID) == true)
                sortingSkins[i].Select();

            openableSkinHandler.AddOpenableSkin(sortingSkins[i]);
            var spawnedSkinView = _skinViewSpawner.GetSkinView(sortingSkins[i]);
            spawnedSkinView.Initialize(sortingSkins[i], this);
            _spawnedSkinsView.Add(spawnedSkinView);
        }

        _shopScroll.Initialize(_spawnedSkinsView);
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
                _skinStateSaver.Save(skin.ID.ToString(), 1);
                return true;
            }
        }

        return false;
    }

    public void Select(int id)
    {
        if (TrySearchByID(id, out Skin skin))
        {
            if (skin.IsBy == true)
            {
                DeselectSkins();
                skin.Select();
                _selectedID = id;
                _selectedSkinIDSaver.Save(SelectedIDKey, _selectedID);
                Selected?.Invoke(_selectedID);
            }
        }
    }

    private bool TrySearchByID(int id, out Skin skin)
    {
        for (int i = 0; i < _skins.Count; i++)
        {
            if (_skins[i].ID == id)
            {
                skin = _skins[i];
                _skinStateSaver.Save(id.ToString(), 1);
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
