using System.Collections.Generic;
using Sourse.Enviroment.Common;
using Sourse.Game.FinishContent;
using UnityEngine;

namespace Sourse.SceneConfigurator
{
    [CreateAssetMenu(fileName = "SceneConfig", menuName = "Scene Config/New Scene Config", order = 52)]
    public class SceneConfig : ScriptableObject
    {
        [SerializeField] private List<Item> _items = new List<Item>();
        [SerializeField] private Finish _finishTemplate;

        [field: SerializeField] public int LevelNumber { get; private set; }

        public IReadOnlyList<Item> Items => _items;
    }
}
