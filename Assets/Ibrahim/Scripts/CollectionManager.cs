using TMPro;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] int collectionCount = 0;
    [SerializeField] int maxCount = 8;
    [SerializeField] TextMeshProUGUI collectionTxt;

    [SerializeField] BoxCollider lastDoorCollider;

    private void Start()
    {
        collectionTxt.text = collectionCount.ToString() + "/" + maxCount.ToString();
    }
    public void CollectedItem()
    {
        collectionCount++;
        collectionTxt.text = collectionCount.ToString() + "/" + maxCount.ToString();

        if (collectionCount >= maxCount)
        {
            lastDoorCollider.enabled = true;
        }
    }
}
