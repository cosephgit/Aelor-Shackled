using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// this low-level base class is used to make an object play dialogue lines and move around
// all interactables and anything else that can act in the world are based on this
// it has hooks for playing animations and showing text, but it does not assume they exist
// Created by: Seph 27/5
// Last edit by: Seph 30/5

public enum AnimSingle
{
    Shock, // trigger "shock"
    Laugh, // trigger "laugh"
    Cry, // trigger "cry"
    Fall, // trigger "fall"
    Die, // trigger "die" - this one does not automatically return to normal anim
    None // does nothing
}

public class ActorBase : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]private SpriteRenderer sprite;
    [SerializeField]private float moveSpeed = 5f;
    [SerializeField]protected Animator animator;
    [Header("Idle actions")]
    [SerializeField]private EventSequence[] idleEvents;
    [SerializeField]private float idleDelay = 10f;
    [Header("Dialogue")]
    [SerializeField]private TextMeshPro text;
    [SerializeField]private Color tint = Color.clear;
    private float idleTimer;
    protected WalkableArea moveAreaCurrent;
    protected Vector3 moveTarget; // the move target to reach the target inside the current collider
    protected WalkableArea moveTargetArea; // the current move's target area
    protected Vector3 moveTargetFinal; // the final move target
    protected WalkableArea moveTargetAreaFinal; // the current move's target area
    private bool moving = false;
    private bool moveEvent = false; // set to true if the actor is required to move during an event
    private float movingCycle = 0f;
    private bool waiting = false; // waiting for battle mode or adventure pause to end
    private Vector3 spriteScale;
    protected bool asleep = false;

    protected virtual void Awake()
    {
        if (text)
        {
            text.enabled = false;
            if (tint.a > 0)
                text.color = tint; // use the tint if it has been changed from clear
            else if (sprite)
                text.color = sprite.color; // else copy the sprite color
            SetIdleTimer();
        }
    }

    private void Start()
    {
        if (sprite)
        {
            spriteScale = sprite.transform.localScale;

            sprite.transform.localScale = spriteScale * SceneManager.instance.GetScaleForYPos(transform.position.y);
        }

        EnterWalkArea(SceneManager.instance.GetClosestWalkable(transform.position, out _));
        ClearMoveTarget();
    }

    // when this actor needs to enter a walkable area, call this method to do so
    private void EnterWalkArea(WalkableArea area)
    {
        sprite.sortingLayerID = area.GetAreaLayer();
        moveAreaCurrent = area;
    }

    public void ClearMoveTarget()
    {
        moveTarget = transform.position - moveAreaCurrent.transform.position;
        moveTargetArea = null;
        moveTargetAreaFinal = null;
    }

    // sets the movement target point for the player
    // this should already be validated within the defined moveable area in the SceneManager
    public void SetMoveTarget(Vector3 pos, bool duringEvent = false)
    {
        if (asleep) return;

        moveTarget = pos - moveAreaCurrent.transform.position; // movement is always assigned RELATIVE TO A WALKABLE AREA
        //moveTarget = pos;
        moveEvent = duringEvent;
        moving = true;
    }

    private void SetMovePath(WalkableArea areaNext, Vector3 pointNext, WalkableArea areaFinal, Vector3 pointfinal, bool duringEvent = false)
    {
        if (asleep) return;

        moveTargetArea = areaNext;
        moveTargetFinal = pointfinal - areaFinal.transform.position;
        moveTargetAreaFinal = areaFinal;
        SetMoveTarget(pointNext, duringEvent);
    }

    // try to find a way for this actor to move to the target point
    // it finds the nearest move area
    // then tries to pathfind to that area from the current area
    // if it finds a path, it sets the actor to move it
    // if it fails to find a path, it tells the actor not to move
    public void TryMove(Vector3 point, bool duringEvent, bool forced = false)
    {
        if (asleep) return;

        if (forced)
        {
            // for events moving outside of the walkable areas
            moveTarget = point - moveAreaCurrent.transform.position;
            moveTargetFinal = point - moveAreaCurrent.transform.position;

            moveTargetArea = null;
            moveTargetAreaFinal = null;

            moving = true;
            return;
        }

        // first check if there's a collider directly under the point
        WalkableArea areaNext; // the next movement area needed to follow this path
        Vector2 pointNext; // the movement point needed to reach the next area in this path
        Vector2 pointValid; // need to adjust the input point to a valid point inside a walkable area
        WalkableArea areaTarget = SceneManager.instance.GetClosestWalkable(point, out pointValid);

        if (areaTarget)
        {
            // the point is in a valid move area, check if it's possible to navigate to it from the current move area
            if (moveAreaCurrent)
            {
                if (areaTarget == moveAreaCurrent)
                {
                    SetMovePath(areaTarget, pointValid, areaTarget, pointValid, duringEvent);
                    return;
                }
                else if (moveAreaCurrent.FindConnection(new List<WalkableArea>(), areaTarget, out pointNext, out areaNext))
                {
                    // a valid connection has been found to this move area
                    // move areas should NOT be overlapped, so assume this is the right final area
                    SetMovePath(areaNext, pointNext, areaTarget, pointValid, duringEvent);
                    return;
                }
            }
            else
            {
                EnterWalkArea(areaTarget);
                SetMovePath(areaTarget, pointValid, areaTarget, pointValid, duringEvent);
                Debug.LogError("actor " + gameObject + " does not have an initial move area set");
            }
        }
    }

    public bool IsMoving()
    {
        return moving;
    }

    // set the delay before an idle event will happen with a small amount of random variation
    private void SetIdleTimer()
    {
        if (idleEvents.Length > 0)
            idleTimer = idleDelay * Random.Range(0.9f, 1.1f);
    }

    public void ShowLine(string line, AnimSingle animToPlay = AnimSingle.None)
    {
        if (text)
        {
            text.enabled = true;
            text.text = line;
        }
        if (animator)
        {
            switch (animToPlay)
            {
                case AnimSingle.Shock:
                {
                    animator.SetTrigger("shock");
                    break;
                }
                case AnimSingle.Laugh:
                {
                    animator.SetTrigger("laugh");
                    break;
                }
                case AnimSingle.Cry:
                {
                    animator.SetTrigger("cry");
                    break;
                }
                case AnimSingle.Fall:
                {
                    animator.SetTrigger("fall");
                    break;
                }
                case AnimSingle.Die:
                {
                    animator.SetTrigger("die");
                    break;
                }
                default:
                case AnimSingle.None:
                {
                    animator.SetBool("talking", true);
                    break;
                }
            }
        }
    }

    public void HideLine()
    {
        if (text)
        {
            text.enabled = false;
        }
        if (animator)
        {
            animator.SetBool("talking", false);
        }
    }

    private void UpdateMoveAnimation(Vector2 move)
    {
        // hacky pretend animation until we have some animations
        if (move.x >= 0)
        {
            if (sprite)
            {
                Vector3 angles = sprite.transform.localEulerAngles;
                angles.z = 5f;
                sprite.transform.localEulerAngles = angles;
                sprite.flipX = false;
            }
            movingCycle += Time.deltaTime;
        }
        else
        {
            if (sprite)
            {
                Vector3 angles = sprite.transform.localEulerAngles;
                angles.z = -5f;
                sprite.transform.localEulerAngles = angles;
                sprite.flipX = true;
            }
            movingCycle -= Time.deltaTime;
        }
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(movingCycle * 20f) * 5f);
        // end of hacky pretend animation
    }

    // ends movement effects when the destination is reached
    private void UpdateMoveAnimationEnd()
    {
        if (sprite)
        {
            Vector3 angles = sprite.transform.localEulerAngles;
            angles.z = 0f;
            sprite.transform.localEulerAngles = angles;
        }
        if (animator)
        {
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", 0);
        }
        moving = false;
        movingCycle = 0f;
        transform.rotation = Quaternion.identity;
        SetIdleTimer();
    }

    // moves this pawn to it's current destination for one Update
    // note that this DOES need to be checked even when not moving, to allow for parallax layers moving under the pawn
    // (possibly actors will be made a child of the parallax layer in future)
    protected void UpdateMove()
    {
        Vector3 offset = moveTarget + moveAreaCurrent.transform.position - transform.position;

        if (Mathf.Approximately(offset.magnitude, 0))
        {
            bool finishedMove = true;

            // have reached the current target point
            // check if we have a final destination point and continue to it if possible
            // otherwise, stop moving
            if (moveTargetArea)
            {
                if (moveTargetArea != moveAreaCurrent)
                {
                    // then this move is not the final move in the path
                    // pathfinding is set up so check for next area
                    if (moveTargetAreaFinal)
                    {
                        EnterWalkArea(moveTargetArea);
                        moveTargetArea = null;
                        finishedMove = false;
                        if (moveAreaCurrent == moveTargetAreaFinal)
                        {
                            // have reached final move target, so just move to the final destination point
                            // remember that SetMovePath expects ABSOLUTE positions not RELATIVE positions
                            SetMovePath(moveTargetAreaFinal, moveTargetFinal + moveTargetAreaFinal.transform.position, moveTargetAreaFinal, moveTargetFinal + moveTargetAreaFinal.transform.position, moveEvent);
                            //moveTarget = moveTargetFinal;
                            //moveTargetAreaFinal = null;
                            finishedMove = false;
                        }
                        else
                        {
                            // there are more areas to move through, find the path
                            // remember that TryMove expects ABSOLUTE positions not RELATIVE positions
                            TryMove(moveTargetFinal + moveTargetAreaFinal.transform.position, moveEvent);
                        }
                    }
                }
            }

            if (finishedMove)
                UpdateMoveAnimationEnd();
        }
        else
        {
            float frameSpeed = moveSpeed * Time.deltaTime; // * SceneManager.instance.GetScaleForYPos(transform.position.y);
            Vector3 move;

            if (offset.magnitude <= frameSpeed)
            {
                move = offset;
            }
            else
            {
                move = offset.normalized * frameSpeed;
            }

            if (animator)
            {
                animator.SetFloat("moveX", move.x);
                animator.SetFloat("moveY", move.y);
            }

            if (moving)
                UpdateMoveAnimation(move);

            move += transform.position;

            // fake distance scaling
            if (sprite)
            {
                Vector3 scale = spriteScale * SceneManager.instance.GetScaleForYPos(move.y);
                sprite.transform.localScale = scale;
            }

            transform.position = move;
        }
    }

    // checks for idle animation and dialogue for an Update
    private void UpdateIdleCheck()
    {
        if (asleep) return;

        if (idleEvents.Length > 0)
        {
            idleTimer -= Time.deltaTime;
            
            if (idleTimer < 0)
            {
                SetIdleTimer();
                idleEvents[Random.Range(0, idleEvents.Length)].Run();
            }
        }
    }

    // update the player position each frame if needed
    private void Update()
    {
        if (SceneManager.instance.adventureState && !SceneManager.instance.adventurePaused)
        {
            if (waiting)
            {
                waiting = false;
                moveEvent = false; // make sure one event move doesn't bleed over into another event by accident
                SetIdleTimer();
            }
            else if (!moving)
            {
                UpdateIdleCheck();
            }
        }
        else if (!waiting)
        {
            // have just entered pause state, so record that the actor is on hold owing to the pause
            waiting = true;
            if (!moveEvent)
            {
                // if NOT set to move during event, clear the current move target
                ClearMoveTarget();
                movingCycle = 0f;
                if (animator)
                {
                    animator.SetFloat("moveX", 0f);
                    animator.SetFloat("moveY", 0f);
                }
                transform.rotation = Quaternion.identity;
            }
        }

        UpdateMove();
    }

    public virtual void Wake()
    {
        asleep = false;
        SetIdleTimer();
    }

    public virtual void Sleep()
    {
        asleep = true;
        HideLine();
        ClearMoveTarget();
    }
}
