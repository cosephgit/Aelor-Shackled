using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a script for placing/removing a particle system/other visual effects
// Created by: Seph 1/6
// Last edit by: Seph 7/6

public class TimedEffect : MonoBehaviour
{
    [SerializeField]private ParticleSystem particles;
    [SerializeField]private float duration; // if > 0 then this item will self-destroy after the duration
    [SerializeField]private SpriteRenderer[] sprites;

    void Awake()
    {
        StopEffects();
    }

    public void PlayEffects()
    {
        if (particles)
            particles.Play();

        for (int i = 0; i < sprites.Length; i++)
            sprites[i].enabled = true;

        if (duration > 0)
        {
            Destroy(gameObject, duration);
        }
    }

    public void StopEffects()
    {
        if (particles)
            particles.Stop();

        for (int i = 0; i < sprites.Length; i++)
            sprites[i].enabled = false;
    }
}