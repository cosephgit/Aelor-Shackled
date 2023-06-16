using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// creates the indicated particle system at the position of the indicated gameobject
// Created by: Seph 8/6
// Last edit by: Seph 8/6

public class EventParticles : Event
{
    [SerializeField]private TimedEffect particles;
    [SerializeField]private Transform target;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);

        TimedEffect particlesNew = Instantiate(particles, target.position, particles.transform.rotation);
        particlesNew.PlayEffects();
    }
}
