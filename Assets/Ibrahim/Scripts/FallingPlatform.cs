using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 0.2f;
    [SerializeField] private float returnDelay = 3f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody rb;

    private bool isFalling;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        startPosition = transform.position;
        startRotation = transform.rotation;

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (isFalling) return;

        StartCoroutine(FallAndReturn());
    }

    private IEnumerator FallAndReturn()
    {
        isFalling = true;

        yield return new WaitForSeconds(fallDelay);

        // Make it fall
        rb.isKinematic = false;
        rb.useGravity = true;

        yield return new WaitForSeconds(returnDelay);

        // Stop physics movement
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Reset position
        rb.isKinematic = true;
        rb.useGravity = false;

        transform.position = startPosition;
        transform.rotation = startRotation;

        isFalling = false;
    }
}