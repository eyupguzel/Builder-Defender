using TMPro;
using UnityEngine;

public class TooltipUI : Singleton<TooltipUI>
{
    [SerializeField] private RectTransform canvasRectTransform;
    private RectTransform rectTransform;
    private TextMeshProUGUI textMesh;
    private RectTransform backgroundRectTransform;

    private Vector2 anchoredPosition;

    TooltipTimer tooltipTimer;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        textMesh = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        Hide();
    }
    private void Update()
    {
        anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if(anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;

        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;

        rectTransform.anchoredPosition = anchoredPosition;

        if (tooltipTimer != null)
        {
            tooltipTimer.timer -= Time.deltaTime;

            if(tooltipTimer.timer <= 0)
                Hide();
        }


    }
    private void SetText(string text)
    {
        textMesh.SetText(text);
        textMesh.ForceMeshUpdate();

        Vector2 textSize = textMesh.GetRenderedValues(false);
        Vector2 padding = new Vector2(10, 8);
        backgroundRectTransform.sizeDelta = textSize + padding;
    }
    public void Show(string text = null, TooltipTimer tooltipTimer = null)
    {
        this.tooltipTimer = tooltipTimer;
        SetText(text);
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
public class TooltipTimer
{
    public float timer;
}
