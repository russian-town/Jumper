using System;
using Sourse.Constants;
using Sourse.Pause;
using Sourse.UI.LevelCompletePanel;
using UnityEngine.SceneManagement;

namespace Sourse.Level
{
    public class LevelLoader
    {
        private PauseView _pauseView;
        private LevelFinishView _levelFinishView;

        public LevelLoader(PauseView pauseView,
            LevelFinishView levelFinishView)
        {
            _pauseView = pauseView;
            _levelFinishView = levelFinishView;
        }

        public void Subscribe()
        {
            _pauseView.ExitButtonClicked += OnExitButtonCliced;
            _pauseView.RestatrButtonClicked += OnRestartButtonCliced;
            _levelFinishView.NextLevelButtonClicked += OnNextLevelButtonClicked;
        }

        public void Unsubscribe() 
        {
            _pauseView.ExitButtonClicked -= OnExitButtonCliced;
            _pauseView.RestatrButtonClicked -= OnRestartButtonCliced;
            _levelFinishView.NextLevelButtonClicked -= OnNextLevelButtonClicked;
        }

        private void OnNextLevelButtonClicked()
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            index++;

            if (SceneManager.GetSceneByBuildIndex(index).IsValid())
                SceneManager.LoadScene(index);
            else
                SceneManager.LoadScene(LevelName.First);
        }

        private void OnRestartButtonCliced()
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(index);
        }

        private void OnExitButtonCliced()
            => SceneManager.LoadScene(LevelName.MainMenu);
    }
}
