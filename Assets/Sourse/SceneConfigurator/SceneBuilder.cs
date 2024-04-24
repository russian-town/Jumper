using System.Collections.Generic;
using Sourse.Enviroment.Common;
using UnityEngine;

namespace Sourse.SceneConfigurator
{
    public class SceneBuilder
    {
        public List<Item> Create(SceneConfig sceneConfig, Ground.Ground groundTemplate)
        {
            List<Item> items = new();
            Vector3 position = Vector3.zero;

            foreach (var itemTemplate in sceneConfig.ItemTemplates)
            {
                Item item = Object.Instantiate(itemTemplate, position, Quaternion.identity);
                items.Add(item);
                position.x += 4.5f;
            }

            int index = items.Count / 2;
            Ground.Ground ground = Object.Instantiate(groundTemplate);
            ground.transform.position = new Vector3(items[index].Position.x, -.25f, 0f);
            ground.transform.localScale = new Vector3(position.x, .5f, 5f);
            return items;
        }
    }
}
 