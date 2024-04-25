using UnityEngine.Events;

namespace Sourse.Enviroment.Common
{
    [System.Serializable]
    public class CollisionEvent<Vector3> : UnityEvent<Vector3>
    {
    }

    [System.Serializable]
    public class CollisionEvent : UnityEvent
    {
    }
}
