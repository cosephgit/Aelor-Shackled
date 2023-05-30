using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event will enable all indicated actors and triggers
// Created by: Seph 30/5
// Last edit by: Seph 30/5

public class EventEnable : Event
{
    [SerializeField]private ActorBase[] subjects;
    [SerializeField]private EventTrigger[] triggers;
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
        }
    }
}
