using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// this class detects mouse clicks/touch taps and implements their effects
// Created by: Seph 27/5
// Last edit by: Seph 7/6

public class UIControlInterface : UIControlInterfaceMenu
{
    public static UIControlInterface instance;
    [field: Header("In game menus")]
    [field: SerializeField]public UIInteractMenu interactionMenu { get; private set; }
    [field: SerializeField]public UIDialogueTree dialogueTree { get; private set; }
    [field: SerializeField]public UIInventory inventory { get; private set; }
    [field: SerializeField]public UIInventoryItemMove inventoryItem { get; private set; }
    [field: SerializeField]public UIFadeOutManager fadeManager { get; private set; }
    //[SerializeField]private Image mousePointer;
    [SerializeField]private AudioClip clickTick;
    private InteractableBase interactable;

    protected override void Awake()
    {
        if (instance)
        {
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        else
            instance = this;

        interactionMenu.gameObject.SetActive(false);

        base.Awake();
    }

    private List<RaycastResult> GetUIObjects(Vector2 pos)
    {
        // check if the object is the interactionmenu
        PointerEventData pointerData = new PointerEventData (EventSystem.current)
        {
            pointerId = -1,
        };

        pointerData.position = pos;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results;
    }

    // called when a position to touch is determined
    // the pos is the UI position, not the world position
    protected override void TouchInput(Vector2 pos, bool tap)
    {
        base.TouchInput(pos, tap);

        // TODO add mouse pointer changes (e.g. highlight over interactables) here

        if (tap && interactionMenu.gameObject.activeSelf)
        {
            bool interactionHide = true;

            SoundSystemManager.instance.PlaySFXStandard(clickTick);

            // if the interaction menu is open, the first tap off it is just to close it
            if (EventSystem.current.IsPointerOverGameObject())
            {
                List<RaycastResult> objectsUI = GetUIObjects(pos);

                for (int i = 0; i < objectsUI.Count; i++)
                {
                    UIInteractMenu interactTouched = objectsUI[i].gameObject.GetComponentInParent<UIInteractMenu>();

                    if (interactTouched)
                        interactionHide = false;
                }
            }

            if (interactionHide)
            {
                interactionMenu.gameObject.SetActive(false);
                tap = false;
            }
        }

        if (SceneManager.instance.adventureState && !SceneManager.instance.adventurePaused)
        {
            // if in active adventure mode, do UI stuff
            if (EventSystem.current.IsPointerOverGameObject())
            {
                List<RaycastResult> objectsUI = GetUIObjects(pos);

                for (int i = 0; i < objectsUI.Count; i++)
                {
                    UIInventory inventory = objectsUI[i].gameObject.GetComponentInParent<UIInventory>();

                    if (inventory) inventory.UIMouseOver();
                }
            }
            else if (tap)
            {
                // only check for touches of game objects if there is no UI element under the mouse
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(pos);
                Collider2D[] touchHits = Physics2D.OverlapPointAll(worldPos, Global.LayerInteract());

                SoundSystemManager.instance.PlaySFXStandard(clickTick);

                foreach (Collider2D touch in touchHits)
                {
                    InteractableBase interactableTest = touch.gameObject.GetComponentInParent<InteractableBase>();

                    if (interactableTest)
                    {
                        interactable = interactableTest;
                        // something has been touched that can be interacted with, trigger the first one found
                        // note there should NOT be multiple interactables stacked up anyway, spread the items out more!
                        interactionMenu.OpenUIMenu(pos, interactable.HasLook(), interactable.HasTalk(), interactable.HasUse(), interactable.HasSpecial());
                        SceneManager.instance.playerAdventure.ClearMoveTarget();
                        return;
                    }
                }

                // nothing has been clicked on, so try telling the player pawn to move to the point
                SceneManager.instance.playerAdventure.TryMove(worldPos, false);
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

    // takes a worldspace point and returns the screenspace point
    // this versions works for RectTransform.anchoredPosition only
    // deprecated
    /*
    public Vector2 WorldToScreenPosForRect(Vector2 pos)
    {
        Vector2 viewportPos = Camera.main.WorldToScreenPoint(pos);
        Vector2 canvasPos = new Vector2(viewportPos.x * canvasScaler.referenceResolution.x / Screen.width,
                                        viewportPos.y * canvasScaler.referenceResolution.y / Screen.height);

        Debug.Log("pos " + pos + " viewportpos " + viewportPos + " canvaspos " + canvasPos);

        return canvasPos;
    }
    */

    // takes a worldspace point and returns the screenspace point
    public Vector2 WorldToScreenPos(Vector2 pos)
    {
        Vector2 viewportPos = Camera.main.WorldToScreenPoint(pos);

        return viewportPos;
    }
}
