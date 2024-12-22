using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Drag : MonoBehaviour
{
    public GameObject UICanvas;
    GraphicRaycaster UIRaycaster;
    PointerEventData clickData;
    List<RaycastResult> clickResults;

    List<GameObject> selectedElements;

    public LayerMask BlockingMask;

    bool dragging = false;
    GameObject dragElement;

    Vector3 mousePos;
    Vector3 prevMousePos;

    void Start()
    {
        UIRaycaster = UICanvas.GetComponent<GraphicRaycaster>();
        clickData = new PointerEventData(EventSystem.current);
        clickResults = new List<RaycastResult>();
        selectedElements = new List<GameObject>();
    }

    void Update()
    {
        MouseDragUi();
    }

    void MouseDragUi()
    {
        /** Houses the main mouse dragging logic. **/

        mousePos = Mouse.current.position.ReadValue();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            DetectUi();
        }

        if (Mouse.current.leftButton.isPressed && dragging)
        {
            DragElement();
        }
        else
        {
            dragging = false;
        }

        prevMousePos = mousePos;
    }

    void DetectUi()
    {
        /** Detect if the mouse has been clicked on a UI element, and begin dragging **/

        GetUIElementsClicked();

        if (selectedElements.Count > 0)
        {
            dragging = true;
            dragElement = selectedElements[0];
        }
    }

    void GetUIElementsClicked()
    {
        /** Get all the UI elements clicked, using the current mouse position and raycasting. **/

        clickData.position = mousePos;
        clickResults.Clear();
        UIRaycaster.Raycast(clickData, clickResults);

        // Optimised version
        //clicked_elements = (from result in click_results select result.gameObject).ToList();        

        bool ignoreMask = false;

        // Foreach version
        selectedElements.Clear();
        foreach (RaycastResult result in clickResults)
        {
            if(ignoreMask) break;

            ignoreMask = (1 << clickResults[0].gameObject.layer) == BlockingMask;

            Debug.Log(result.gameObject.name + ignoreMask);

            if (result.gameObject.TryGetComponent(out Selectable selectable))
                selectedElements.Add(selectable.gameObject);
        }
    }

    void DragElement()
    {
        /** Drag a UI element across the screen based on the mouse movement. **/

        RectTransform element_rect = dragElement.GetComponent<RectTransform>();

        Vector2 drag_movement = mousePos - prevMousePos;
        element_rect.anchoredPosition = element_rect.anchoredPosition + drag_movement;
    }

    Vector3 KeepFullyOnScreen(GameObject panel, Vector3 newPos)
    {
        RectTransform rect = panel.GetComponent<RectTransform>();
        RectTransform CanvasRect = UICanvas.GetComponent<RectTransform>();

        float minX = (CanvasRect.sizeDelta.x - rect.sizeDelta.x) * -0.5f;
        float maxX = (CanvasRect.sizeDelta.x - rect.sizeDelta.x) * 0.5f;
        float minY = (CanvasRect.sizeDelta.y - rect.sizeDelta.y) * -0.5f;
        float maxY = (CanvasRect.sizeDelta.y - rect.sizeDelta.y) * 0.5f;

        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        return newPos;
    }
}
