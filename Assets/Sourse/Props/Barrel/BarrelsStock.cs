using System.Collections.Generic;
using UnityEngine;

namespace Sourse.Props.Barrel
{
    public class BarrelsStock : MonoBehaviour
    {
        [SerializeField] private List<Barrel> _barrels = new List<Barrel>();

        public void AddPhysicsBarrels()
        {
            foreach (var barrel in _barrels)
            {
                barrel.AddPhysics();
            }
        }
    }
}
