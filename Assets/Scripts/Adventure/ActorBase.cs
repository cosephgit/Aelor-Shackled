using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// this low-level base class is used to make an object play dialogue lines and move around
// all interactables and anything else that can act in the world are based on this
// it has hooks for playing animations and showing text, but it does not assume they exist

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
    [SerializeField]private float playerSpeed = 4f;
    [SerializeField]protected Animator animator;
    [Header("Idle actions")]
    [SerializeField]private EventSequence[] idleEvents;
    [SerializeField]private float idleDelay = 10f;
    [Header("Dialogue")]
    [SerializeField]private TextMeshPro text;
    private float idleTimer;
    private Vector3 moveTarget;
    private bool moving = false;
    private bool moveEvent = false; // set to true if the actor is required to move during an event
    private float movingCycle = 0f;
    private bool waiting = false; // waiting for battle mode or adventure pause to end
    private Vector3 spriteScale;

    protected virtual void Awake()
    {
        if (text)
        {
            text.enabled = false;
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
    }

    // sets the movement target point for the player
    // this should already be validated within the defined moveable area in the SceneManager
    public void SetMoveTarget(Vector2 pos, bool duringEvent = false)
    {
        moveTarget = pos;
        moveEvent = duringEvent;
        moving = true;
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

    // moves this pawn to it's current destination for one Update
    protected void UpdateMove()
    {
        Vector3 offset = moveTarget - transform.position;

        if (Mathf.Approximately(offset.magnitude, 0))
        {
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
        else
        {
            float frameSpeed = playerSpeed * Time.deltaTime; // * SceneManager.instance.GetScaleForYPos(transform.position.y);
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

            // hacky pretend animation until we have some artwork
            if (move.x > 0)
            {
                if (sprite)
                {
                    Vector3 scale = sprite.transform.localEulerAngles;
                    scale.z = 5f;
                    sprite.transform.localEulerAngles = scale;
                }
                movingCycle -= Time.deltaTime;
            }
            else if (move.x < 0)
            {
                if (sprite)
                {
                    Vector3 scale = sprite.transform.localEulerAngles;
                    scale.z = -5f;
                    sprite.transform.localEulerAngles = scale;
                }
                movingCycle += Time.deltaTime;
            }
            else
                movingCycle += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(movingCycle * 20f) * 5f);
            // end of hacky pretend animation

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
        if (SceneManager.instance.GetAdventureActive())
        {
            if (waiting)
            {
                waiting = false;
                moveEvent = false; // make sure one event move doesn't bleed over into another event by accident
                SetIdleTimer();
            }
            else if (moving)
            {
                UpdateMove();
            }
            else
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
                moveTarget = transform.position;
                movingCycle = 0f;
                if (animator)
                {
                    animator.SetFloat("moveX", 0f);
                    animator.SetFloat("moveY", 0f);
                }
                transform.rotation = Quaternion.identity;
            }
        }
        else if (moveEvent)
        {
            // allow moving during an event if required
            UpdateMove();
        }
    }
}
