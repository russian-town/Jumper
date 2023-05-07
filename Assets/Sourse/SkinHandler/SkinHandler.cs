using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Saver), typeof(Sorter))]
public abstract class SkinHandler : MonoBehaviour
{
    protected const string IsByKey = "IsBy";
    protected const string IsSelectKey = "IsSelect";

    [SerializeField] private List<Skin> _skins = new List<Skin>();

    private Saver _saver;
    private Sorter _sorter;

    protected Saver Saver => _saver;

    protected IReadOnlyList<Skin> Skins => _skins;

    protected virtual void Initialize()
    {
        _saver = GetComponent<Saver>();
        _sorter = GetComponent<Sorter>();
        _sorter.SortingSkins(ref _skins);

        foreach (var skin in _skins)
        {
            skin.SetSaveData(_saver.GetState(IsByKey, skin.ID), _saver.GetState(IsSelectKey, skin.ID));
        }
    }
}
