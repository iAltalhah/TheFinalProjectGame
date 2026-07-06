using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] private GameObject rewindImage;
    [SerializeField] private GameObject tripleJumpImage;
    [SerializeField] private GameObject floatImage;

    private void Awake()
    {
        SetRewindImage(false);
        SetTripleJumpImage(false);
        SetFloatImage(false);
    }

    public void SetRewindImage(bool active)
    {
        if (rewindImage != null)
            rewindImage.SetActive(active);
    }

    public void SetTripleJumpImage(bool active)
    {
        if (tripleJumpImage != null)
            tripleJumpImage.SetActive(active);
    }

    public void SetFloatImage(bool active)
    {
        if (floatImage != null)
            floatImage.SetActive(active);
    }
}