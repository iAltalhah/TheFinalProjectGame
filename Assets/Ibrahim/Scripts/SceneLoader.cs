using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName = "scene name";
    [SerializeField] private GameObject confirmPanel;

    [Header("Optional")]
    [SerializeField] private CharacterInput3rdPerson playerInput;

    private bool panelOpen;

    private void Awake()
    {
        if (confirmPanel != null)
            confirmPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (panelOpen) return;

        OpenConfirmPanel();
    }

    private void OpenConfirmPanel()
    {
        panelOpen = true;

        if (confirmPanel != null)
            confirmPanel.SetActive(true);

        if (playerInput != null)
            playerInput.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    public void YesLoadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void NoGoBack()
    {
        panelOpen = false;

        if (confirmPanel != null)
            confirmPanel.SetActive(false);

        if (playerInput != null)
            playerInput.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }
}