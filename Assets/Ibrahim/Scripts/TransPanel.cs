
using UnityEngine;
using UnityEngine.UI;

public class TransPanel : MonoBehaviour
{
    Image imagePanel;
    Color color;

    private void Start()
    {
        imagePanel = GetComponent<Image>();

        color = imagePanel.color;
        color.a = 0f;
        imagePanel.color = color;
    }

    public void TransToBlack()
    {
        color = imagePanel.color;
        color.a = 1f;
        imagePanel.color = color;
    }

    public void TransBack()
    {
        color = imagePanel.color;
        color.a = 0f;
        imagePanel.color = color;
    }
}