using Unity.VisualScripting;
using UnityEngine;

public class RazanCylinder : MonoBehaviour
{
   
    public float speed = 2.0f;     
    public float distance = 3.0f;  
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float movement = Mathf.Sin(Time.time * speed) * distance;
        transform.position = startPosition + new Vector3(0, 0, movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name + " tag: " + other.tag);

        if (other.CompareTag("Player"))
        {
            Debug.Log("bbb");
        }
    }
}
