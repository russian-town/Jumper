using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Saver))]
public class TutorialPosition : MonoBehaviour
{
    [SerializeField] private Tutorial _tutorial;
    [SerializeField] private TutorialType _tutorialType;
    [SerializeField] private bool _isLastPostion;

    private bool _isComplete = false;
    private BoxCollider _boxCollider;
    private Saver _saver;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
        _saver = GetComponent<Saver>();
        _isComplete = _saver.GetTutorialState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isComplete == true)
            return;

        if (other.TryGetComponent(out Player player))
        {
            _tutorial.Show(_tutorialType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isComplete == true)
            return;

        if (other.TryGetComponent(out Player player))
        {
            _tutorial.Hide(_tutorialType);

            if (_isLastPostion == true)
            {
                _isComplete = true;
                _saver.SaveTutorialState(_isComplete);
            }
        }
    }
}
