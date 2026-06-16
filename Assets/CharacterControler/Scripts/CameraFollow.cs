using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    public Vector3 targetOffset = new Vector3(0f, 2f, 0f);

    [Header("Camera")]
    public float distance = 5f;
    public float lookSensitivity = 0.15f;
    public float minPitch = -20f;
    public float maxPitch = 70f;

    [Header("Follow")]
    public float followSmooth = 20f;

    private Vector2 lookVector;
    private float yaw;
    private float pitch = 20f;
    private Vector3 smoothedTargetPosition;

    private void Start()
    {
        if (target == null)
        {
            return;
        }

        yaw = transform.eulerAngles.y;
        smoothedTargetPosition = target.position + targetOffset;
    }

    public void InputLookVector(Vector2 newLookVector)
    {
        lookVector = newLookVector;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        // Mouse/right stick directly controls camera angles.
        yaw += lookVector.x * lookSensitivity;
        pitch -= lookVector.y * lookSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Vector3 targetPosition = target.position + targetOffset;

        smoothedTargetPosition = Vector3.Lerp(
            smoothedTargetPosition,
            targetPosition,
            followSmooth * Time.deltaTime
        );

        Quaternion cameraRotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 cameraPosition =
            smoothedTargetPosition - cameraRotation * Vector3.forward * distance;

        transform.position = cameraPosition;
        transform.rotation = cameraRotation;
    }
}