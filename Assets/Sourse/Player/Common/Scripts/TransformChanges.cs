using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class TransformChanges
    {
        public Vector3 Positon {  get; private set; }

        public TransformChanges(Vector3 positon)
            => Positon = positon;
    }
}
