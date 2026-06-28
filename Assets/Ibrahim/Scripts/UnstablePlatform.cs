using UnityEngine;

public class UnstablePlatform : MonoBehaviour
{

    [SerializeField] private float speed = 1.5f;      // Speed of the oscillation
    [SerializeField] private float height = 0.2f;   // Scale of the movement

    void Update()
    {
        // Calculate the tiny movement change for this exact frame
        float moveAmount = Mathf.Cos(Time.time * speed) * height * Time.deltaTime;

        // Apply the movement directly to the Y-axis
        transform.Translate(0, moveAmount, 0, Space.World);

    }
}
