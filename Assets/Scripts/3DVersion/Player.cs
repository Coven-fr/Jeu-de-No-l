using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInputActions inputs;

    bool isPressed;

    private void Awake()
    {
        inputs = new();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            inputs.Disable();
        }
        else
        {
            inputs.Enable();
        }
    }

    void OnClickPerformed(InputAction.CallbackContext ctx)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                
        if (hit)
        {
            if (hit.collider.TryGetComponent(out Selectable selectable))
            {
                Debug.Log("Discover!");

                selectable.Discover();
            }
        }
        else
        {
            isPressed = true;
        }
    }

    void OnClickCanceled(InputAction.CallbackContext ctx)
    {
        isPressed = false;
    }

    void OnZoomPerformed(InputAction.CallbackContext ctx)
    {
        var mouseWheel = ctx.ReadValue<Vector2>().normalized;
        var scroll = mouseWheel.y;

        GameEvent.current.onMouseZoom?.Invoke(scroll);
    }

    void OnPanPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isPressed)
        {
            Vector2 panAxis = ctx.ReadValue<Vector2>();

            GameEvent.current.onPan?.Invoke(panAxis);
        }
    }

    private void OnEnable()
    {
        inputs.Hunt.Click.performed += OnClickPerformed;
        inputs.Hunt.Click.canceled += OnClickCanceled;
        inputs.Hunt.Zoom.performed += OnZoomPerformed;
        inputs.Hunt.Pan.performed += OnPanPerformed;
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Hunt.Click.performed -= OnClickPerformed;
        inputs.Hunt.Click.canceled -= OnClickCanceled;
        inputs.Hunt.Zoom.performed -= OnZoomPerformed;
        inputs.Hunt.Pan.performed -= OnPanPerformed;
        inputs.Disable();
    }
}
