using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float damp = 10f;
    [SerializeField] private Vector3 offset = Vector3.back;

    private void LateUpdate()
    {
        if (target == null) return;

        var newPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, damp * Time.deltaTime);
    }

    public void SwitchTarget(Transform t)
    {
        target = t;
    }
}