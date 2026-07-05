using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesLogic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI liveCounterTxt;
    [SerializeField] int liveCounter = 5;

    [SerializeField] string previousScene = "scene name";

    private void Start()
    {
        liveCounterTxt.text = liveCounter.ToString();
    }

    public void PlayerFell()
    {
        liveCounter--;
        liveCounterTxt.text = liveCounter.ToString();
        if (liveCounter <= 0)
        {
            SceneManager.LoadScene(previousScene);
        }
    }
}
