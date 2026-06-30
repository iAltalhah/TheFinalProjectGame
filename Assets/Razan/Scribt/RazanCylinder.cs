using UnityEngine;

public class RazanCylinder : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name + " tag: " + other.tag);

        if (other.CompareTag("Player"))
        {
            Debug.Log("bbb");
        }
    }
}
