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

        public void Play(float duration, Func<float, TransformChanges> body)
        {
            if (_lastAnimation != null)
                _context.StopCoroutine(Get(duration, body));

            _context.StartCoroutine(Get(duration, body));
        }

        private IEnumerator Get(float duration, Func<float, TransformChanges> body)
        {
            float expiredSeconds = 0f;
            float progress = 0f;

            while (progress < 1f)
            {
                expiredSeconds += Time.deltaTime;
                progress = expiredSeconds / duration;
                LastChanges = body.Invoke(progress);
                yield return null;
            }

            _lastAnimation = null;
        }
    }
}
