using TMPro;
using UnityEngine;

public class LivesLogic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI liveCounterTxt;
    [SerializeField] int liveCounter = 5;

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
            Debug.Log("player died");
            // load the first scene
        }
    }
}
