using UnityEngine;
using UnityEngine.UI;

public class ScrollList : MonoBehaviour
{
	ScrollRect scrollRect => GetComponent<ScrollRect>();

	[SerializeField] RectTransform content;
    Vector2 prevContentPos;
	[SerializeField] float padding = 1f;

	public Slider Slider;

    public void UpdateScrollRect()
	{
        scrollRect.verticalNormalizedPosition = Slider.value;
    }

    public void UpdateSlider()
    {
        float scrollPos = scrollRect.verticalNormalizedPosition;
        Slider.value = scrollPos;
    }

    public void ShowElement(RectTransform element)
    {
        Vector2 elementPos = element.anchoredPosition;
        float contentHeight = content.rect.height;
        float scrollHeight = scrollRect.viewport.rect.height;

        float targetPos = Mathf.Clamp01((-elementPos.y + padding) / (contentHeight - scrollHeight));

        Debug.Log("Target:" + targetPos);

        scrollRect.verticalNormalizedPosition = 1 - targetPos;
    }
}
