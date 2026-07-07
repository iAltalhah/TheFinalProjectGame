 using UnityEngine;
using UnityEngine.EventSystems;
public class Ranad_ExitButton : MonoBehaviour, IPointerEnterHandler
{
    public RectTransform button;
    public RectTransform canvas;

    public void OnPointerEnter(PointerEventData eventData)
    {
        float x = Random.Range(
            -canvas.rect.width / 2 + button.rect.width / 2,
             canvas.rect.width / 2 - button.rect.width / 2);

        float y = Random.Range(
            -canvas.rect.height / 2 + button.rect.height / 2,
             canvas.rect.height / 2 - button.rect.height / 2);

        button.anchoredPosition = new Vector2(x, y);
    }
}