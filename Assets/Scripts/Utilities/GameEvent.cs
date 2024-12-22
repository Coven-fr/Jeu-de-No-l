using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static GameEvent current;

    void Awake()
    {
        current = this;   
    }

    public delegate void CameraZoomCallback(float zoomValue);
    public CameraZoomCallback onMouseZoom;
    public CameraZoomCallback onSliderZoom;
    public CameraZoomCallback onUpdateZoom;

    public delegate void PanCallback(Vector2 value);
    public PanCallback onPan;

}
