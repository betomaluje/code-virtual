using UnityEngine;

public class TestMousePosition : MonoBehaviour
{
    [SerializeField] private MousePosition3D mousePosition;
    [SerializeField] private Transform prefab;

    private void OnEnable()
    {
        mousePosition.OnMouseClick += HandleMouseClick;
    }

    private void OnDisable()
    {
        mousePosition.OnMouseClick -= HandleMouseClick;
    }

    private void HandleMouseClick(Vector3 obj)
    {
        Instantiate(prefab, obj, Quaternion.identity);
    }

    private void LateUpdate()
    {
        transform.position = mousePosition.MousePosition;
    }
}
