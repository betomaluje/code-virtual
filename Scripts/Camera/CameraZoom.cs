using UnityEngine;

[DisallowMultipleComponent]
public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float minZoom = 65;
    [SerializeField] private float maxZoom = 20;
    [SerializeField] private float sensitivity = 1;
    [SerializeField] private float speed = 60;

    private float _targetZoom;

    private void Awake()
    {
        if (mainCamera != null)
            _targetZoom = mainCamera.orthographicSize;
    }

    private void Update()
    {
        if (mainCamera == null) return;

        _targetZoom -= Input.mouseScrollDelta.y * sensitivity;
        _targetZoom = Mathf.Clamp(_targetZoom, minZoom, maxZoom);

        mainCamera.orthographicSize = Mathf.MoveTowards(
            mainCamera.orthographicSize,
            _targetZoom,
            speed * Time.deltaTime
        );
    }
}
