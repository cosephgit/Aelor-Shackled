using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdventureController : TalkableBase
{
    [Header("Player specific features")]
    [SerializeField]private SpriteRenderer playerSprite;
    [SerializeField]private Animator playerAnimator;
    [SerializeField]private float playerSpeed = 4f;
    [Header("Idle actions")]
    [SerializeField]private EventSequence[] idleEvents;
    [SerializeField]private float idleDelay = 10f;
    private Vector3 moveTarget;
    private bool moving = false;
    private float movingCycle = 0f;
    private float idleTimer;
    private bool waiting = false; // waiting for battle mode or adventure pause to end


    protected override void Awake()
    {
        base.Awake();
        SetIdleTimer();
    }

    // set the delay before an idle event will happen with a small amount of random variation
    private void SetIdleTimer()
    {
        idleTimer = idleDelay * Random.Range(0.9f, 1.1f);
    }

    // sets the movement target point for the player
    // this should already be validated within the defined moveable area in the SceneManager
    public void SetMoveTarget(Vector2 pos)
    {
        moveTarget = pos;
        moving = true;
    }

    // update the player position each frame if needed
    private void Update()
    {
        if (SceneManager.instance.GetAdventureActive())
        {
            if (waiting)
            {
                waiting = false;
                SetIdleTimer();
            }
            else if (moving)
            {
                Vector3 offset = moveTarget - transform.position;

                if (Mathf.Approximately(offset.magnitude, 0))
                {
                    playerAnimator.SetFloat("moveX", 0);
                    playerAnimator.SetFloat("moveY", 0);
                    moving = false;
                    movingCycle = 0f;
                    transform.rotation = Quaternion.identity;
                    SetIdleTimer();
                }
                else
                {
                    float frameSpeed = playerSpeed * Time.deltaTime;
                    Vector3 move;

                    if (offset.magnitude <= frameSpeed)
                    {
                        move = offset;
                    }
                    else
                    {
                        move = offset.normalized * frameSpeed;
                    }

                    playerAnimator.SetFloat("moveX", move.x);
                    playerAnimator.SetFloat("moveY", move.y);

                    // hacky pretend animation until we have some artwork
                    if (move.x > 0)
                    {
                        Vector3 scale = playerSprite.transform.localEulerAngles;
                        scale.z = 5f;
                        playerSprite.transform.localEulerAngles = scale;
                        movingCycle -= Time.deltaTime;
                    }
                    else if (move.x < 0)
                    {
                        Vector3 scale = playerSprite.transform.localEulerAngles;
                        scale.z = -5f;
                        playerSprite.transform.localEulerAngles = scale;
                        movingCycle += Time.deltaTime;
                    }
                    else
                        movingCycle += Time.deltaTime;
                    transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(movingCycle * 20f) * 5f);
                    // end of hacky pretend animation

                    move += transform.position;

                    transform.position = move;
                }
            }
            else
            {
                idleTimer -= Time.deltaTime;
                
                if (idleTimer < 0)
                {
                    SetIdleTimer();
                    idleEvents[Random.Range(0, idleEvents.Length)].Run();
                }
            }
        }
        else if (!waiting)
        {
            // have just entered pause state, so reset everything here
            waiting = true;
            moveTarget = transform.position;
            movingCycle = 0f;
            transform.rotation = Quaternion.identity;
        }
    }
}
