using System.Collections;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Range(0f, 1f)] private float rotationDuration = .8f;
    [SerializeField] private float degrees = 90f;

    private void Update()
    {
        // depending on the input rotate + - 90 degrees on the y axis
        float toRotateDegrees = target.rotation.eulerAngles.y;

        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(RotateMe(Vector3.up * degrees, rotationDuration));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(RotateMe(Vector3.up * -degrees, rotationDuration));
        }
    }

    private IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = target.rotation;
        var toAngle = Quaternion.Euler(target.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            target.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }
}
