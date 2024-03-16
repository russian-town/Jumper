using System.Collections;
using UnityEngine;

namespace Sourse.Tutorial
{
    [RequireComponent(typeof(TutorialAnimaton), typeof(TutorialView))]
    public class Tutorial : MonoBehaviour
    {
        private const string DoubleClickKey = "DoubleClick";
        private const string ClickKey = "Click";

        [SerializeField] private float _delay;

        private TutorialAnimaton _tutorialAnimaton;
        private TutorialView _tutorialView;
        private bool _isWork = true;

        private void Awake()
        {
            _tutorialAnimaton = GetComponent<TutorialAnimaton>();
            _tutorialView = GetComponent<TutorialView>();
            _tutorialAnimaton.Initialize();
            _tutorialView.Initialize();
            _tutorialView.Hide();
        }

        public void Show(TutorialType tutorialType)
        {
            _tutorialView.Show();
            StartCoroutine(StartTutorial(tutorialType));
        }

        public void Hide(TutorialType tutorialType)
        {
            StopCoroutine(StartTutorial(tutorialType));
            _tutorialView.Hide();
        }

        private IEnumerator StartTutorial(TutorialType tutorialType)
        {
            WaitForSeconds delay = new WaitForSeconds(_delay);

            while (_isWork)
            {
                switch (tutorialType)
                {
                    case TutorialType.Clik:
                        _tutorialAnimaton.Clik(ClickKey);
                        break;
                    case TutorialType.DoubleClick:
                        _tutorialAnimaton.Clik(DoubleClickKey);
                        break;
                }  

                yield return delay;
            }
        }
    }
}
