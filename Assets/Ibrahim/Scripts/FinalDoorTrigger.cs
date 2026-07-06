using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDoorTrigger : MonoBehaviour
{
    [SerializeField] string sceneName = "Scene Name";


    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(sceneName);
    }
}
