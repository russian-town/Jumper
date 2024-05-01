using System;
using System.Collections;
using UnityEngine;

namespace Sourse.Player.Common.Scripts
{
    public class PureAnimation
    {
        private readonly MonoBehaviour _context;

        private Coroutine _lastAnimation;

        public PureAnimation(MonoBehaviour context)
            => _context = context;

        public TransformChanges LastChanges { get; private set; }

        public void Play(AnimationCurve duration, Func<float, TransformChanges> body)
            => _lastAnimation = _context.StartCoroutine(Get(duration, body));

        public void Stop()
        {
            if (_lastAnimation != null)
                _context.StopCoroutine(_lastAnimation);
        }

        private IEnumerator Get(AnimationCurve duration, Func<float, TransformChanges> body)
        {
            float expiredSeconds = 0f;
            float progress = 0f;

            while (progress < 1f)
            {
                expiredSeconds += Time.deltaTime;
                progress = expiredSeconds / duration.Evaluate(expiredSeconds);
                LastChanges = body.Invoke(progress);
                yield return null;
            }

            _lastAnimation = null;
        }
    }
}
