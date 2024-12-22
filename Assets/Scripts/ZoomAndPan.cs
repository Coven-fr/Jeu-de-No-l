using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndPan : MonoBehaviour
{
    public Camera mainCamera;

    [SerializeField] RectTransform imageRectTransform;  // Référence à l'UI Image (RectTransform)
    public float zoomSpeed = 0.1f;            // Vitesse de zoom
    public float minZoom = 0.5f;              // Zoom minimum
    public float maxZoom = 2.0f;              // Zoom maximum
    Vector2 initialPosition = Vector2.zero;  // Position initiale

    private Vector2 dragStartPos;
    private Vector2 imageStartPos;

    void Update()
    {
        HandleZoom();
        HandleDrag();
    }

    void HandleZoom()
    {
        // Zoom avec la molette de la souris (ou un autre moyen de zoom)
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float newScale = imageRectTransform.localScale.x + scrollInput * zoomSpeed;

        // Limite du zoom
        newScale = Mathf.Clamp(newScale, minZoom, maxZoom);

        // Appliquer le zoom
        imageRectTransform.localScale = new Vector3(newScale, newScale, 1);

        mainCamera.orthographicSize -= scrollInput * zoomSpeed;
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);

        // Si on dézoome à la taille originale (ou en dessous d'un seuil), réinitialiser la position à (0, 0)
        if (newScale <= 1f)
        {
            imageRectTransform.localPosition = initialPosition;
        }
    }

    void HandleDrag()
    {
        // Déplacer l'image avec le clic gauche de la souris (drag)
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = Input.mousePosition;
            imageStartPos = imageRectTransform.localPosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 dragDelta = (Vector2)Input.mousePosition - dragStartPos;
            imageRectTransform.localPosition = imageStartPos + dragDelta;
        }
    }
}
