using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event causes one actor to be disabled
// Created by: Seph 30/5
// Last edit by: Seph 30/5

public class EventDisable : Event
{
    [SerializeField]private ActorBase[] subjects;
    [SerializeField]private EventTrigger[] triggers;
    [SerializeField]private bool disableEntirely = true; // if true, the subjects will be turned off entirely, otherwise it will only stop them being interactive (i.e. still visible)

    public override void End()
    {
        base.End();
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
