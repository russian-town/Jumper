using UnityEngine;

public class Screenshoter : MonoBehaviour
{
    private int _lastScreenshotNumber;

    private void Start()
    {
        if (PlayerPrefs.HasKey("LastNumber"))
            _lastScreenshotNumber = PlayerPrefs.GetInt("LastNumber");
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ScreenCapture.CaptureScreenshot($"Screenshot{_lastScreenshotNumber}.png");
            _lastScreenshotNumber++;
            PlayerPrefs.SetInt("LastNumber", _lastScreenshotNumber);
            PlayerPrefs.Save();
        }
    }
}
