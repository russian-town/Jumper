using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraScaler : MonoBehaviour
{
    [Range(0f, 1f)][SerializeField] private float _widthOrHeight = 0;
    [SerializeField] private Vector2 _defaultResolution;

    private Camera _camera;
    private float _targetAspect;
    private float _initialFov;
    private float _horizontalFov;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Start()
    {
        _targetAspect = _defaultResolution.x / _defaultResolution.y;
        _initialFov = _camera.fieldOfView;
        _horizontalFov = CalculateVerticalFov(_initialFov, 1 / _targetAspect);
    }

    private void Update()
    {
        float constantWidthFov = CalculateVerticalFov(_horizontalFov, _camera.aspect);
        _camera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFov, _widthOrHeight);
    }

    private float CalculateVerticalFov(float horizontalFovInDeg, float aspectRatio)
    {
        float horizontalFovInRad = horizontalFovInDeg * Mathf.Deg2Rad;
        float verticalFovInRads = 2f * Mathf.Atan(Mathf.Tan(horizontalFovInRad / 2f) / aspectRatio);
        return verticalFovInRads * Mathf.Rad2Deg;
    }
}
