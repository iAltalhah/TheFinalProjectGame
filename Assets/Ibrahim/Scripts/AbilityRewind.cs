using System;
using System.Collections;
using UnityEngine;

public class AbilityRewind : MonoBehaviour
{
    [SerializeField] Transform rewindAnchor;
    [SerializeField] float rewindDuration = 0.5f;

    CharacterInput3rdPerson input;
    CharacterControllerBase characterController;

    bool isRewinding;

    void Awake()
    {
        input = GetComponent<CharacterInput3rdPerson>();
        characterController = GetComponent<CharacterControllerBase>();
    }

    public bool StartRewind(Action onFinished = null)
    {
        if (isRewinding)
            return false;

        if (rewindAnchor == null)
        {
            Debug.LogWarning("Rewind anchor is missing.");
            return false;
        }

        StartCoroutine(RewindToAnchor(onFinished));
        return true;
    }

    IEnumerator RewindToAnchor(Action onFinished)
    {
        isRewinding = true;

        if (input != null)
            input.enabled = false;

        if (characterController != null)
        {
            characterController.CancelFloatBecauseOfRewind();
            characterController.StopMovement();
            characterController.enabled = false;
        }

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

        if (characterController != null)
        {
            characterController.enabled = true;
            characterController.StopMovement();
        }

        if (input != null)
            input.enabled = true;

        isRewinding = false;

        onFinished?.Invoke();
    }
}