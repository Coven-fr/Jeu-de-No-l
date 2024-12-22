using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zoom : MonoBehaviour
{
    [SerializeField] Slider zoomSlider;

    [SerializeField] Selectable selectable;

    [SerializeField] private float zoomOutMin;
    [SerializeField] private float zoomOutMax;

    float previousZoomSliderValue;

    private void Start()
    {
        zoomSlider.minValue = zoomOutMin;
        zoomSlider.maxValue = zoomOutMax;
    }

    private void Update()
    {
        float MouseWheelAxis = Input.GetAxis("Mouse ScrollWheel");

        if (MouseWheelAxis != 0)
        {
            ApplyZoom(MouseWheelAxis);
        }
    }

    public void UseZoomSlider()
    {
        transform.localScale = new Vector2(zoomSlider.value, zoomSlider.value);

        if(zoomSlider.value < previousZoomSliderValue)
            ResetRectTransform();

        previousZoomSliderValue = zoomSlider.value;
    }

    void ApplyZoom(float increment)
    {
        float ClampX = Mathf.Clamp(transform.localScale.x + increment, zoomOutMin, zoomOutMax);
        float ClampY = Mathf.Clamp(transform.localScale.y + increment, zoomOutMin, zoomOutMax);

        transform.localScale = new Vector2(ClampX, ClampY);

        zoomSlider.value = ClampX;

        float sign = Mathf.Sign(increment);

        Debug.Log("Sign:" + sign);

        if (sign < 0) ResetRectTransform();
    }

    void ResetRectTransform()
    {
        Debug.Log("Reset");

        RectTransform rect = selectable.GetComponent<RectTransform>();

        float step = zoomSlider.value / 0.1f;
        float increment = Mathf.Abs(rect.anchoredPosition.x / step);

        rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, new Vector2(0,0), increment);

        Debug.Log(rect.position.x + " // " + rect.position.y);
    }
}
