using UnityEngine;

[CreateAssetMenu(fileName = "New skin", menuName = "Skins", order = 59)]
public class Skin : ScriptableObject
{
    private const int DefaultSkinOrder = 0;
    private const int PaidSkinOrder = 1;
    private const int OpeningSkinOrder = 2;
    private const int RewardedSkinOrder = 3;

    [SerializeField] private SkinType _type;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _price;
    [SerializeField] private int _id;
    [SerializeField] private bool _isSelect;
    [SerializeField] private bool _isBought;

    private int _sortOrder;
    private int _minPrice = 0;

    public SkinType Type => _type;
    public Sprite Icon => _icon;
    public int Price => _price;
    public int ID => _id;
    public int SortOrder => _sortOrder;
    public bool IsSelect => _isSelect;
    public bool IsBought => _isBought;

    private void OnValidate()
    {
        switch (_type)
        {
            case SkinType.Default:
                SetStartParameter(_minPrice, DefaultSkinOrder, true);
                break;
            case SkinType.Paid:
                SetStartParameter(_price, PaidSkinOrder, _isBought);
                break;
            case SkinType.Rewarded:
                SetStartParameter(_minPrice, RewardedSkinOrder, _isBought);
                break;
            case SkinType.Openable:
                SetStartParameter(_minPrice, OpeningSkinOrder, _isBought);
                break;
        }
    }

    public void Buy() => _isBought = true;

    public void Select() => _isSelect = true;

    public void Deselect() => _isSelect = false;

    public void SetSaveData(bool isBought, bool isSelect)
    {
        _isSelect = isSelect;

        if (_type == SkinType.Default)
            return;

        _isBought = isBought;
    }

    private void SetStartParameter(int price, int sortOrder, bool isBought)
    {
        _price = price;
        _sortOrder = sortOrder;
        _isBought = isBought;
    }
}
