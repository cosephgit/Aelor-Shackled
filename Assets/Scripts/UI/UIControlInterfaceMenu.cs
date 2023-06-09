using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this basic class just handles the mouse pointer for the menu
// Created by: Seph 1/6
// Last edit by: Seph 9/6

public class UIControlInterfaceMenu : MonoBehaviour
{
    [Header("Universal menu features")]
    [SerializeField]private Transform mousePointer;
    public bool pointerPressed { get; private set; } // used to store whether the mouse is held OR the touchscreen is touched

    protected virtual void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    protected virtual void TouchInput(Vector2 pos, bool tap)
    {
        mousePointer.position = pos;
    }

    // check for touches and mouse clicks each frame
    private void Update()
    {
        if (Input.mousePresent)
        {
            // if there's a mouse, it takes control of the pointer
            pointerPressed = Input.GetMouseButton(0);

            Vector3 touchPos = Input.mousePosition;
            TouchInput(touchPos, (Input.GetMouseButtonDown(0)));

            // detect stretching/shrinking/dragging
        }
        else if (Input.touchCount > 0)
        {
            pointerPressed = true;

            // no mouse, so try for touch controls
            if (Input.touchCount == 1)
            {
                Vector3 touchPos = Input.touches[0].position;
                TouchInput(touchPos, true);
            }
            else
            {
                // detect stretching/shrinking/dragging
            }
        }
        else
            pointerPressed = false;
    }
}
