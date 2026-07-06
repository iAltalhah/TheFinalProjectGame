using UnityEngine;

public class UnstablePlatform : MonoBehaviour
{
    [Header("Random Speed Range")]
    [SerializeField] private float minSpeed = 1.2f;
    [SerializeField] private float maxSpeed = 1.8f;

    [Header("Random Height Range")]
    [SerializeField] private float minHeight = 0.15f;
    [SerializeField] private float maxHeight = 0.3f;

    private float speed;
    private float height;
    private float phaseOffset;

    private Vector3 startPosition;

    void Awake()
    {
        startPosition = transform.position;

        speed = Random.Range(minSpeed, maxSpeed);
        height = Random.Range(minHeight, maxHeight);

        // Makes each platform start at a different point in the wave
        phaseOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * speed + phaseOffset) * height;

        transform.position = startPosition + new Vector3(0f, yOffset, 0f);
    }
}