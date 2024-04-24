using System.Collections.Generic;
using Sourse.Enviroment.Common;
using Sourse.Game.FinishContent;
using UnityEngine;

namespace Sourse.SceneConfigurator
{
    [CreateAssetMenu(fileName = "SceneConfig", menuName = "Scene Config/New Scene Config", order = 52)]
    public class SceneConfig : ScriptableObject
    {
        [SerializeField] private List<Item> _itemTemplates = new ();
        [SerializeField] private List<float> _spaces = new ();

        [field: SerializeField] public int LevelNumber { get; private set; }

        public IReadOnlyList<Item> ItemTemplates => _itemTemplates;

        public IReadOnlyCollection<float> Spaces => _spaces;

        private void OnValidate()
        {
            if (_spaces.Count != _itemTemplates.Count)
                _spaces.Capacity = _itemTemplates.Count;
        }
    }
}
