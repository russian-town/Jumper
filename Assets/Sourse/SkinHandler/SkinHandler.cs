using System.Collections.Generic;
using Sourse.UI.Shop.Scripts;
using Sourse.UI.Shop.SkinConfiguration;
using UnityEngine;

namespace Sourse.SkinHandler
{
    [RequireComponent(typeof(BubbleSort))]
    public abstract class SkinHandler : MonoBehaviour
    {
        protected const string IsByKey = "IsBy";
        protected const string IsSelectKey = "IsSelect";

        [SerializeField] private List<SkinConfig> _skins = new List<SkinConfig>();

       // private Saver.Saver _saver;
        private BubbleSort _sorter;

        //protected Saver.Saver Saver => _saver;

        protected IReadOnlyList<SkinConfig> Skins => _skins;

        protected virtual void Initialize()
        {
            //_saver = GetComponent<Saver.Saver>();
            _sorter = GetComponent<BubbleSort>();
            _sorter.SortingSkins(ref _skins);

            //foreach (var skin in _skins)
            //{
            //    skin.SetSaveData(_saver.GetState(IsByKey, skin.ID), _saver.GetState(IsSelectKey, skin.ID));
            //}
        }
    }
}
