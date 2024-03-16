using System.Collections.Generic;
using Sourse.UI.Shop.Scripts;
using Sourse.UI.Shop.Skin;
using UnityEngine;

namespace Sourse.SkinHandler
{
    [RequireComponent(typeof(Saver.Saver), typeof(Sorter))]
    public abstract class SkinHandler : MonoBehaviour
    {
        protected const string IsByKey = "IsBy";
        protected const string IsSelectKey = "IsSelect";

        [SerializeField] private List<Skin> _skins = new List<Skin>();

        private Saver.Saver _saver;
        private Sorter _sorter;

        protected Saver.Saver Saver => _saver;

        protected IReadOnlyList<Skin> Skins => _skins;

        protected virtual void Initialize()
        {
            _saver = GetComponent<Saver.Saver>();
            _sorter = GetComponent<Sorter>();
            _sorter.SortingSkins(ref _skins);

            foreach (var skin in _skins)
            {
                skin.SetSaveData(_saver.GetState(IsByKey, skin.ID), _saver.GetState(IsSelectKey, skin.ID));
            }
        }
    }
}
