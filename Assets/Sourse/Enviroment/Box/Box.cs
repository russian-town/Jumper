using System.Collections.Generic;
using Sourse.Enviroment.Common;
using UnityEngine;

namespace Sourse.Enviroment
{
    public class Box : Item
    {
        [SerializeField] private List<Rigidbody> _parts = new ();

        public override void Initialize()
        {
            foreach (var part in _parts)
                part.isKinematic = false;
        }
    }
}
