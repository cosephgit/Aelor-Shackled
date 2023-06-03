using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event will enable all indicated actors and triggers
// Created by: Seph 1/6
// Last edit by: Seph 1/6

public class EventEnable : Event
{
    [SerializeField]private ActorBase[] subjects; // actors/interactables that should be enabled/disabled
    [SerializeField]private EventTrigger[] triggers; // area/tier triggers that should be enabled/disabled
    [SerializeField]private TimedEffect[] effects; // particle effects that should be enabled/disabled
    [SerializeField]private bool enableOrDisable = true; // if true this event enables the targets, otherwise it disables them
    [SerializeField]private bool disableEntirely = true; // if true, the subjects will be turned off entirely, otherwise it will only stop them being interactive (i.e. still visible)

    public override void End()
    {
        base.End();
        if (enableOrDisable)
        {
            for (int i = 0; i < subjects.Length; i++)
            {
                subjects[i].gameObject.SetActive(true);
                subjects[i].Wake();
            }
            for (int i = 0; i < triggers.Length; i++)
            {
                triggers[i].gameObject.SetActive(true);
                triggers[i].Reset();
            }
            for (int i = 0; i < effects.Length; i++)
            {
                effects[i].gameObject.SetActive(true);
                effects[i].PlayEffects();
            }
        }
        else
        {
            for (int i = 0; i < subjects.Length; i++)
            {
                if (disableEntirely)
                    subjects[i].gameObject.SetActive(false);
                else
                    subjects[i].Sleep();
            }
            for (int i = 0; i < triggers.Length; i++)
                triggers[i].gameObject.SetActive(false);
            for (int i = 0; i < effects.Length; i++)
                effects[i].StopEffects();
        }
    }
}
