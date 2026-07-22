using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Normal Fall")]
    [SerializeField] private float fallDelay = 0.2f;
    [SerializeField] private float returnDelay = 3f;

    [Header("Countdown Fall")]
    [SerializeField] private bool fallForeverWhenCountdownEnds = false;

    [Header("Visual Warning")]
    [SerializeField] private Color warningBaseColor = Color.red;
    [SerializeField] private Color warningEmissionColor = Color.red;
    [SerializeField] private float warningEmissionStrength = 3f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody rb;

    private Renderer[] platformRenderers;
    private MaterialPropertyBlock propertyBlock;

    private bool isFalling;
    private bool fellForever;

    public bool FallForeverWhenCountdownEnds => fallForeverWhenCountdownEnds;

    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // This gets renderers from the children too.
        platformRenderers = GetComponentsInChildren<Renderer>();
        propertyBlock = new MaterialPropertyBlock();

        startPosition = transform.position;
        startRotation = transform.rotation;

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        ResetPlatformColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (isFalling) return;
        if (fellForever) return;

        StartCoroutine(FallAndReturn());
    }

    private IEnumerator FallAndReturn()
    {
        isFalling = true;

        SetPlatformWarningColor();

        yield return new WaitForSeconds(fallDelay);

        MakePlatformFall();

        yield return new WaitForSeconds(returnDelay);

        if (fellForever)
            yield break;

        ResetPlatform();

        isFalling = false;
    }

    public void FallForever()
    {
        if (fellForever) return;

        fellForever = true;
        isFalling = true;

        StopAllCoroutines();

        SetPlatformWarningColor();
        MakePlatformFall();
    }

    private void MakePlatformFall()
    {
        if (rb == null) return;

        rb.isKinematic = false;
        rb.useGravity = true;
    }

    private void ResetPlatform()
    {
        if (rb == null) return;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;
        rb.useGravity = false;

        transform.position = startPosition;
        transform.rotation = startRotation;

        ResetPlatformColor();
    }

    private void SetPlatformWarningColor()
    {
        if (platformRenderers == null) return;

        foreach (Renderer rend in platformRenderers)
        {
            if (rend == null) continue;

            rend.GetPropertyBlock(propertyBlock);

            propertyBlock.SetColor(BaseColor, warningBaseColor);
            propertyBlock.SetColor(EmissionColor, warningEmissionColor * warningEmissionStrength);

            rend.SetPropertyBlock(propertyBlock);
        }
    }

    private void ResetPlatformColor()
    {
        if (platformRenderers == null) return;

        foreach (Renderer rend in platformRenderers)
        {
            if (rend == null) continue;

            // This removes the custom color from this renderer only.
            // It returns the renderer back to the shared material look.
            rend.SetPropertyBlock(null);
        }
    }
}