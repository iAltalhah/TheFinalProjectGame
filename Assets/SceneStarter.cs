using System.Collections;
using UnityEngine;

public class SceneStarter : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private GameObject introCamera;
    [SerializeField] private GameObject playerCamera;

    [Header("Player")]
    [SerializeField] private CharacterInput3rdPerson characterInput;
    [SerializeField] private CharacterControllerBase characterController;

    [Header("Timing")]
    [SerializeField] private float introDuration = 3f;

    private void Start()
    {
        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        // Disable player control
        if (characterController != null)
        {
            characterController.enabled = false;
        }

        if (characterInput != null)
        {
            characterInput.enabled = false;
        }

        // Enable intro camera, disable player camera
        if (introCamera != null)
        {
            introCamera.SetActive(true);
        }

        if (playerCamera != null)
        {
            playerCamera.SetActive(false);
        }

        // Wait during intro
        yield return new WaitForSeconds(introDuration);

        // Disable intro camera, enable player camera
        if (introCamera != null)
        {
            introCamera.SetActive(false);
        }

        if (playerCamera != null)
        {
            playerCamera.SetActive(true);
        }

        // Enable player control again
        if (characterController != null)
        {
            characterController.enabled = true;
        }

        if (characterInput != null)
        {
            characterInput.enabled = true;
        }
    }
}