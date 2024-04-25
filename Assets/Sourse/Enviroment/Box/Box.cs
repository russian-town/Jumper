using Sourse.Enviroment.Common;
using UnityEngine;

namespace Sourse.Enviroment
{
    [RequireComponent(typeof(BoxCollider))]
    public class Box : Item
    {
        private BoxCollider _boxCollider;

        public override void Initialize()
        {
            _boxCollider = GetComponent<BoxCollider>();
        }

        protected override Collider GetCollider()
        {
            return _boxCollider;
        }
    }
}
