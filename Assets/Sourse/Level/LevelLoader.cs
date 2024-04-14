using Sourse.Constants;
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
    }
}
