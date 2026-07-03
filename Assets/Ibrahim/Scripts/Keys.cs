using UnityEngine;

public class Keys : MonoBehaviour
{
    [SerializeField] CollectionManager collectionManager;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        collectionManager.CollectedItem();
        Destroy(gameObject);
    }
}
