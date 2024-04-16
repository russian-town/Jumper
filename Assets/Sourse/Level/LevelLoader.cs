using Sourse.Constants;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sourse.Level
{
    public class LevelLoader
    {
        public int GetCurrentNumber()
        {
            if (int.TryParse(SceneManager.GetActiveScene().name, out int number))
                return number;

            return LevelName.FirstNumber;
        }

        public void Restart()
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        }

        public void GoNext()
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            index++;

            if (SceneManager.GetSceneByBuildIndex(index).IsValid())
                SceneManager.LoadScene(index);
            else
                SceneManager.LoadScene(LevelName.First);
        }

        public void ExitToMainMenu()
            => SceneManager.LoadScene(LevelName.MainMenu);

        public void OpenShop()
            => SceneManager.LoadScene(LevelName.Shop);

        public Coroutine StartGoToFirstLevel(MonoBehaviour context, Action<float> onProgress)
        {
            return context.StartCoroutine(GoToFirstLevel(onProgress));
        }

        private IEnumerator GoToFirstLevel(Action<float> onProgress)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(LevelName.First);
            operation.allowSceneActivation = false;

            while (operation.progress < .9f)
            {
                onProgress?.Invoke(operation.progress);
                yield return null;
            }

            operation.allowSceneActivation = true;
        }
    }
}
