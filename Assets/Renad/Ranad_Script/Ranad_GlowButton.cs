using UnityEngine;
using UnityEngine.UI;

public class Ranad_GlowButton : MonoBehaviour
{
    public Image image;
    public Color color1 = Color.white;
    public Color color2 = Color.hotPink;
    public float speed = 2f;

    void Update()
    {
        image.color = Color.Lerp(color1, color2, (Mathf.Sin(Time.time * speed) + 1) / 2);
    }
}
