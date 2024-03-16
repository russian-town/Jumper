using UnityEngine;

[CreateAssetMenu(fileName = "New skin", menuName = "Skins", order = 59)]
public class Skin : ScriptableObject
{
    [SerializeField] private SkinType _type;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _price;
    [SerializeField] private int _id;
    [SerializeField] private bool _isSelect;
    [SerializeField] private bool _isBy;

    private int _sortOrder;

    public SkinType Type => _type;
    public Sprite Icon => _icon;
    public int Price => _price;
    public int ID => _id;
    public int SortOrder => _sortOrder;
    public bool IsSelect => _isSelect;
    public bool IsBy => _isBy;

    private void OnValidate()
    {
        switch (_type)
        {
            case SkinType.Default:
                SetStartParametr(0, 0, true);
                break;
            case SkinType.Paid:
                SetStartParametr(_price, 2, _isBy);
                break;
            case SkinType.Rewarded:
                SetStartParametr(0, 3, _isBy);
                break;
            case SkinType.Openable:
                SetStartParametr(0, 1, _isBy);
                break;
        }
    }

    public void By() => _isBy = true;

    public void Select() => _isSelect = true;

    public void Deselect() => _isSelect = false;

    public void SetSaveData(bool isBy, bool isSelect)
    {
        _isSelect = isSelect;

        if (_type == SkinType.Default)
            return;

        _isBy = isBy;
    }

    private void SetStartParametr(int price, int sortOrder, bool isBy)
    {
        _price = price;
        _sortOrder = sortOrder;
        _isBy = isBy;
    }
}
