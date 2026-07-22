using UnityEngine;
using TMPro;

public class PlatformCountdownFallManager : MonoBehaviour
{
    [Header("Countdown")]
    [SerializeField] private float countdownTime = 60f;
    [SerializeField] private TMP_Text countdownText;

    private float currentTime;
    private bool countdownFinished;

    private FallingPlatform[] platforms;

    private void Start()
    {
        currentTime = countdownTime;

        // Find all FallingPlatform scripts once at the start
        platforms = FindObjectsOfType<FallingPlatform>();

        UpdateCountdownUI();
    }

    private void Update()
    {
        if (countdownFinished) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            countdownFinished = true;

            UpdateCountdownUI();
            MakeMarkedPlatformsFallForever();

            return;
        }

        UpdateCountdownUI();
    }

    private void UpdateCountdownUI()
    {
        if (countdownText == null) return;

        int seconds = Mathf.CeilToInt(currentTime);
        countdownText.text = seconds.ToString();
    }

    private void MakeMarkedPlatformsFallForever()
    {
        foreach (FallingPlatform platform in platforms)
        {
            if (platform.FallForeverWhenCountdownEnds)
            {
                platform.FallForever();
            }
        }

        Debug.Log("Countdown finished. Marked platforms are falling forever.");
    }
}