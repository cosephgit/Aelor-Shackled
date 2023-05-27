using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// this class detects mouse clicks/touch taps and implements their effects

public class UIControlInterface : MonoBehaviour
{
    [SerializeField]private UIInteractMenu interactionMenu;
    [SerializeField]private Image mousePointer;
    private InteractableBase interactable;

    private void Awake()
    {
        interactionMenu.gameObject.SetActive(false);
    }

    // called when a position to touch is determined
    // the pos is the UI position, not the world position
    void TouchInput(Vector2 pos, bool tap)
    {
        mousePointer.transform.position = pos;

        // TODO add mouse pointer changes (e.g. highlight over interactables) here

        if (tap & SceneManager.instance.GetAdventureActive())
        {
            // if the player has tapped/clicked and we're in adventure mode, try to do adventure stuff

            if (EventSystem.current.IsPointerOverGameObject()){ } // only check for touches of game objects if there is no UI element under the mouse
            else
            {
                if (interactionMenu.gameObject.activeSelf)
                {
                    // if the interaction menu is open, the first tap off it is just to close it
                    interactionMenu.gameObject.SetActive(false);
                }
                else
                {
                    Vector2 worldPos = Camera.main.ScreenToWorldPoint(pos);
                    Collider2D[] touchHits = Physics2D.OverlapPointAll(worldPos, Global.LayerInteract());

                    foreach (Collider2D touch in touchHits)
                    {
                        InteractableBase interactableTest = touch.gameObject.GetComponent<InteractableBase>();

                        if (interactableTest)
                        {
                            interactable = interactableTest;
                            // something has been touched that can be interacted with, trigger the first one found
                            // note there should NOT be multiple interactables stacked up anyway, spread the items out more!
                            interactionMenu.OpenUIMenu(pos, interactable.HasLook(), interactable.HasTalk(), interactable.HasUse(), interactable.HasSpecial());
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
    }

    // check for touches and mouse clicks each frame
    private void Update()
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

    public void SelectLook()
    {
        if (interactable)
        {
            interactable.DoLook();
        }
    }
    public void SelectTalk()
    {
        if (interactable)
        {
            interactable.DoTalk();
        }
    }
    public void SelectUse()
    {
        if (interactable)
        {
            interactable.DoUse();
        }
    }
    public void SelectSpecial()
    {
        if (interactable)
        {
            interactable.DoSpecial();
        }
    }
}
