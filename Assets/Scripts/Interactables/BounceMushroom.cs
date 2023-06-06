using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used for the scene 2 bounce mushroom puzzle
// Created by: Seph 5/6
// Last edit by: Seph 6/6

public class BounceMushroom : MonoBehaviour
{
    [SerializeField]private SpriteRenderer sprite;
    [SerializeField]private float bouncedScale = 0.8f;
    [SerializeField]private Color colorBounced = Color.white;
    private Color colorInitial;
    private Vector3 scaleStart; // the initial scale of the mushroom object
    private Vector3 scaleBounced; // the initial scale of the mushroom object
    private bool bounced; // whether this mushroom is "bounced" or not

    private void Awake()
    {
        scaleStart = transform.localScale;
        colorInitial = sprite.color;
        scaleBounced = scaleStart;
        scaleBounced.y *= bouncedScale;
    }

    public void SetBounced(bool bouncedSet)
    {
        if (bouncedSet != bounced)
        {
            bounced = bouncedSet;
            if (bounced)
            {
                transform.localScale = scaleBounced;
                sprite.color = colorBounced;
            }
            else
            {
                transform.localScale = scaleStart;
                sprite.color = colorInitial;
            }

            // TODO play bounce sound
        }
    }
}
