using System;
using UnityEngine;

public class MousePosition3D : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask groundLayer;

    public Action<Vector3> OnMouseClick;

    public Vector3 MousePosition => _mousePosition;

    private Vector3 _mousePosition;

    private void Update()
    {
        _mousePosition = GetMouseWorldPosition();

        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClick?.Invoke(_mousePosition);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, groundLayer))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
