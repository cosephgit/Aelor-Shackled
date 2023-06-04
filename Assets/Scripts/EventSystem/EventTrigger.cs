using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is a univeral trigger for events
// if given a collider, it will trigger on the player if they enter the collider
// if the timer is set, it will trigger automatically when the timer runs out
// Created by: Seph 30/5
// Last edit by: Seph 30/5

public class EventTrigger : MonoBehaviour
{
    [SerializeField]private BoxCollider2D triggerArea;
    [SerializeField]private float triggerTimer;
    [SerializeField]private EventSequence triggerEvent;
    [SerializeField]private bool oneShot = false; // set true if this trigger should activate only once
    private float timeCountdown;
    private bool active = true;

    void Awake()
    {
        if (triggerTimer > 0) timeCountdown = triggerTimer;
    }

    private void Trigger()
    {
        triggerEvent.Run();
        active = false;
    }

    private void Update()
    {
        if (active)
        {
            // triggers only operate during adventure mode
            if (SceneManager.instance.adventureState && !SceneManager.instance.adventurePaused)
            {
                if (timeCountdown > 0)
                {
                    timeCountdown -= Time.deltaTime;
                    if (timeCountdown <= 0)
                    {
                        Trigger();
                        return;
                    }
                }
                if (triggerArea)
                {
                    if (triggerArea.OverlapPoint(SceneManager.instance.playerAdventure.transform.position))
                    {
                        Trigger();
                        return;
                    }
                }
            }
        }
    }

    // reset this trigger so it can be triggered again
    public void Reset()
    {
        active = true;
        if (triggerTimer > 0) timeCountdown = triggerTimer;
    }
}
