using UnityEngine;

namespace Sourse.Tutorial
{
    public class TutorialView : MonoBehaviour
    {
        [SerializeField] private TutorialText _tutorialText;
        [SerializeField] private TutorialImage _tutorialImage;

        public void Initialize()
        {
            _tutorialText.Initialize();
        }

        public void Show()
        {
            _tutorialText.Initialize();
            _tutorialText.Show();
            _tutorialImage.Show();
        }

        public void Hide()
        {
            _tutorialText.Hide();
            _tutorialImage.Hide();
        }
    }
}
