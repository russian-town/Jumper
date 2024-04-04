using Sourse.Constants;
using UnityEngine.SceneManagement;

namespace Sourse.Level
{
    public class LevelLoader
    {
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
    }
}
