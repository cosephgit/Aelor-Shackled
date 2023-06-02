using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a script for placing/removing a particle system/other visual effects
// Created by: Seph 1/6
// Last edit by: Seph 1/6

public class TimedEffect : MonoBehaviour
{
    [SerializeField]private ParticleSystem particles;
    [SerializeField]private float duration; // if > 0 then this item will self-destroy after the duration

    public void PlayEffects()
    {
        if (particles)
            particles.Play();

        if (duration > 0)
        {
            Destroy(gameObject, duration);
        }
    }

    public void StopEffects()
    {
        if (particles)
            particles.Stop();
    }
}