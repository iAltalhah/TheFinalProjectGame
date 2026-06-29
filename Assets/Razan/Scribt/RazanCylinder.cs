using UnityEngine;

public class RazanCylinder : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("bbb");
        }
    }
}

