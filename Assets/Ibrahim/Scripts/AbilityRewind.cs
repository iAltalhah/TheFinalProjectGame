using System.Collections;
using UnityEngine;

public class AbilityRewind : MonoBehaviour
{
    [SerializeField] Transform rewindAnchor;
    [SerializeField] float rewindDuration = 0.5f;

    CharacterInput3rdPerson ci;
    CapsuleCollider cc;

    public bool canRewind = false;
    bool isRewinding;

    void Start()
    {
        ci = GetComponent<CharacterInput3rdPerson>();
        cc = GetComponent<CapsuleCollider>();
    }

    public void TryRewind()
    {
        if (!isRewinding && canRewind)
        {
            StartCoroutine(RewindToAnchor());
        }
    }

    IEnumerator RewindToAnchor()
    {
        if (rewindAnchor == null)
            yield break;

        isRewinding = true;

        ci.enabled = false;
        cc.enabled = false;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = rewindAnchor.position;

        float elapsedTime = 0f;

        while (elapsedTime < rewindDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / rewindDuration;
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        transform.position = targetPosition;

        ci.enabled = true;
        cc.enabled = true;

        isRewinding = false;
    }

    public void CanRewind()
    {
        canRewind = true;
    }
}