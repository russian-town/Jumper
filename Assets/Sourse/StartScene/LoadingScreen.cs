using Sourse.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sourse.StartScene
{
    public class LoadingScreen
    {
        private readonly LevelLoader _levelLoader;
        private readonly MonoBehaviour _context;
        private readonly Queue<ILoadingOperation> _loadingOperations;

        private float _targetProgress;
        private Coroutine _loadind;

        public LoadingScreen(Queue<ILoadingOperation> loadingOperations,
            MonoBehaviour context,
            LevelLoader levelLoader)
        {
            _loadingOperations = loadingOperations;
            _context = context;
            _levelLoader = levelLoader;
        }

        public event Action<ILoadingOperation> LoadingOperationChanged;
        public event Action<float> ProgressChanged;

        public void OnProgress(float progress)
        {
            _targetProgress = progress;
            ProgressChanged?.Invoke(progress);
        }

        public Coroutine StartLoad()
        {
            if (_loadind != null)
                return null;

            _loadind = _context.StartCoroutine(Load());
            return _loadind;
        }

        private IEnumerator Load()
        {
            while(_loadingOperations.Count > 0)
            {
                ResetProgress();
                var loadingOperation = _loadingOperations.Dequeue();
                yield return loadingOperation.Load(OnProgress);
            }

            ResetProgress();
            yield return _levelLoader.StartGoToFirstLevel(_context, OnProgress);
        }

        private void ResetProgress()
        {
            _targetProgress = 0f;
            ProgressChanged?.Invoke(_targetProgress);
        }
    }
}
