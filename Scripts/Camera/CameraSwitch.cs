using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Transform[] cameraTargets;
    [SerializeField] private SimpleCameraFollow cameraFollow;

    private int _currentIndex;
    private int _maxCameras;

    private void Awake()
    {
        _maxCameras = cameraTargets.Length - 1;
        _currentIndex = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // change camera
            _currentIndex++;

            if (_currentIndex > _maxCameras)
            {
                _currentIndex = 0;
            }

            var newTarget = cameraTargets[_currentIndex];
            cameraFollow.SwitchTarget(newTarget);
        }
    }
}
