using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// this class detects mouse clicks/touch taps and implements their effects

public class ControlInterface : MonoBehaviour
{
    [SerializeField]private Image mousePointer;

    // called when a position to touch is determined
    // the pos is the UI position, not the world position
    void TouchInput(Vector2 pos, bool tap)
    {
        mousePointer.transform.position = pos;

        // TODO add mouse pointer changes (e.g. highlight over interactables) here

        if (tap)
        {
            if (EventSystem.current.IsPointerOverGameObject()) { } // only check for touches of game objects if there is no UI element under the mouse
            else
            {
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(pos);
                Collider2D[] touchHits = Physics2D.OverlapPointAll(worldPos, Global.LayerInteract());

                foreach (Collider2D touch in touchHits)
                {
                    InteractableBase interactable = touch.gameObject.GetComponent<InteractableBase>();

                    if (interactable)
                    {
                        // something has been touched that can be interacted with, trigger the first one found
                        // note there should NOT be multiple interactables stacked up, spread items out more!
                        interactable.Touch();
                        return;
                    }
                }

                // no interactables have been found at the touch point, so assume that the player wants to move to the clicked point
                // need to convert the point to a point in the moveable play area
                Vector2 movePoint = SceneManager.instance.moveArea.ClosestPoint(worldPos);
                SceneManager.instance.playerAdventure.SetMoveTarget(movePoint);
            }
        }
    }

    // check for touches and mouse clicks each frame
    void Update()
    {
        if (Input.mousePresent)
        {
            // if there's a mouse, it takes control of the pointer
            Vector3 touchPos = Input.mousePosition;
            TouchInput(touchPos, (Input.GetMouseButtonDown(0)));

            // detect stretching/shrinking/dragging
        }
        else if (Input.touchCount > 0)
        {
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
    }
}
