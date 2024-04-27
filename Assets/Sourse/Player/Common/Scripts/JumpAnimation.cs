using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class JumpAnimation : MonoBehaviour
    {
        private PureAnimation _playtime;

        public void Initialize()
            => _playtime = new PureAnimation(this);

        public PureAnimation Play(Transform jumper, float duration, float height, AnimationCurve animationCurve)
        {
            _playtime.Play(duration, (float progress) =>
            {
                Vector3 position = Vector3.Scale(new Vector3(0f, height * animationCurve.Evaluate(progress), 0f), jumper.up);
                return new TransformChanges(position);
            });

            return _playtime;
        }
    }
}
