using UnityEngine;
using UnityEngine.EventSystems;

public class RunAwayButton : MonoBehaviour, IPointerEnterHandler
{
    public RectTransform canvasRect;
    public AudioSource audioSource;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        float maxX = canvasRect.rect.width / 2f - rectTransform.rect.width / 2f;
        float maxY = canvasRect.rect.height / 2f - rectTransform.rect.height / 2f;

        Vector2 randomPos = new Vector2(
            Random.Range(-maxX, maxX),
            Random.Range(-maxY, maxY)
        );

        rectTransform.anchoredPosition = randomPos;

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}