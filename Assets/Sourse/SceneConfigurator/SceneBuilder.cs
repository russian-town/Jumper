using System.Collections.Generic;
using Sourse.Enviroment.Common;
using Sourse.Ground;
using Sourse.Game.FinishContent;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Sourse.SceneConfigurator
{
    public class SceneBuilder
    {
        private readonly List<Item> _items = new();

        public List<Item> Create(SceneConfig sceneConfig, DeadZone deadZoneTemplate)
        {
            Vector3 position = Vector3.zero;
            Item deadZone = Object.Instantiate(deadZoneTemplate);
            deadZone.Initialize();

            foreach (var itemTemplate in sceneConfig.ItemTemplates)
            {
                Item item = Object.Instantiate(itemTemplate);
                item.Initialize();             
                position.x += Random.Range(3f, 5f);
                position.y = deadZone.transform.position.y + deadZone.transform.localScale.y + item.transform.localScale.y / 2f;
                item.transform.position = position;
                _items.Add(item);
            }

            int index = _items.Count / 2;
            deadZone.transform.position = new Vector3(_items[index].Position.x, 0f, 0f);
            deadZone.transform.localScale += new Vector3(position.x * 5f, .5f, 5f);
            _items.Add(deadZone);
            return _items;
        }

        public Finish GetFinish() 
        {
            foreach (var item in _items) 
            {
                if(item.GetType() == typeof(Finish))
                {
                    return item as Finish;
                }
            }

            return null;
        }

        public DeadZone GetDeadZone()
        {
            foreach(var item in _items)
            {
                if (item.GetType() == typeof(DeadZone))
                {
                    return item as DeadZone;
                }
            }

            return null;
        }
    }
}
 