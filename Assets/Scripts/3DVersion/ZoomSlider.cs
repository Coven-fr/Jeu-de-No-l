using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomSlider : Slider 
{
    protected override void Start()
    {
        CameraZoomAndPan mainCam;

        if (Camera.main.TryGetComponent(out mainCam))
        {
            value = Camera.main.orthographicSize;

            minValue = mainCam.MinZoom;
            maxValue = mainCam.MaxZoom;
        }        

        onValueChanged.AddListener(delegate { Zoom(); });

        GameEvent.current.onUpdateZoom += UpdateSlider;
    }

    void Zoom()
    {
        GameEvent.current.onSliderZoom?.Invoke(value);
    }

    void UpdateSlider(float newValue)
    {
        value = newValue;
    }
}
