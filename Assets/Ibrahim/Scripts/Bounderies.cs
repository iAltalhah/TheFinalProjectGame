using System.Collections;
using UnityEngine;

public class Bounderies : MonoBehaviour
{
    [SerializeField] private Transform safePoint;
    [SerializeField] private CharacterControllerBase characterControllerBase;
    [SerializeField] private CharacterInput3rdPerson characterInput;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float teleportDelay = 2f;

    private bool isTeleporting;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (isTeleporting) return;

        StartCoroutine(TeleportAfterDelay(other));
    }

    private IEnumerator TeleportAfterDelay(Collider other)
    {
        isTeleporting = true;

        Debug.Log("Player touched boundaries.");

        // Stop movement first
        if (characterControllerBase != null)
        {
            characterControllerBase.enabled = false;
        }

        // Disable input too, so the player cannot move during the delay
        if (characterInput != null)
        {
            characterInput.enabled = false;
        }

        yield return new WaitForSeconds(teleportDelay);

        if (safePoint == null)
        {
            Debug.LogWarning("Safe point is missing!");

            EnablePlayerController();

            isTeleporting = false;
            yield break;
        }

        // Teleport the player
        if (playerRigidbody != null)
        {
            playerRigidbody.position = safePoint.position;
            playerRigidbody.linearVelocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
        }
        else
        {
            other.transform.position = safePoint.position;
        }

        // Stop again after teleport, just to make sure no old velocity remains
        if (characterControllerBase != null)
        {
            characterControllerBase.StopMovement();
        }

        EnablePlayerController();

        Debug.Log("Player returned to safe point!");

        isTeleporting = false;
    }

    private void EnablePlayerController()
    {
        if (characterControllerBase != null)
        {
            characterControllerBase.enabled = true;
        }

        if (characterInput != null)
        {
            characterInput.enabled = true;
        }
    }
}