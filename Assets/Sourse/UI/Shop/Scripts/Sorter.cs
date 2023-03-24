using System.Collections.Generic;
using UnityEngine;

public class Sorter : MonoBehaviour
{
    public List<Skin> SortingSkins(List<Skin> skins)
    {
        for (int i = 0; i < skins.Count; i++)
        {
            for (int j = i + 1; j < skins.Count; j++)
            {
                if (skins[i].SortOrder > skins[j].SortOrder)
                {
                    var skin = skins[i];
                    skins[i] = skins[j];
                    skins[j] = skin;
                }
            }
        }

        return skins;
    }
}
