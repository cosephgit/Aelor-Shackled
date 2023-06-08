using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An event for the bounce sequencing through the mushroom patch
// Created by: Seph 6/6
// Last edit by: Seph 6/6

public class EventMushroomBounce : Event
{
    [SerializeField]private ActorBase bounceActor; // the actor that will be bouncing
    [SerializeField]private BounceMushroom[] bounceSequence; // the sequence of mushrooms to bounce on in order
    [SerializeField]private BounceMushroomManager bounceMushroomManager;
    [SerializeField]private MushroomState bounceEndMushroomState = MushroomState.None;
    [SerializeField]private Transform bounceEnding; // an object defining the end point of the bounce sequence
    [SerializeField]private float bounceSpeed = 10f; // the linear speed moving from bounce point to bounce point
    [SerializeField]private float bounceHeightVirtual = 4f; // the amount of virtual height to apply on each bounce
    [SerializeField]private AudioClip bounceSound;
    int bounceCurrent;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);
        finished = false;
        bounceCurrent = 0;
        // start the first bounce in the sequence
        StartCoroutine(Bounce(bounceActor.transform.position, bounceSequence[0].transform.position));
    }

    private IEnumerator Bounce(Vector3 start, Vector3 end)
    {
        float progress = 0f;
        float distance = (end - start).magnitude; // the total linear distance of the bounce
        float bounceTime = distance / bounceSpeed; // the total time this bounce will take

        bounceActor.SetBouncing(true);

        if (bounceSound && bounceCurrent > 0)
            SoundSystemManager.instance.PlayVariedSFX(bounceSound);

        while (progress < bounceTime)
        {
            Vector3 pos;
            progress += Time.deltaTime;
            pos = Vector3.Lerp(start, end, (progress / bounceTime));
            pos.y += Mathf.Sin(progress / bounceTime * Mathf.PI) * bounceHeightVirtual;

            bounceActor.transform.position = pos;

            yield return new WaitForEndOfFrame();
        }

        bounceActor.transform.position = end;

        bounceCurrent++;

        bounceActor.SetBouncing(false);

        if (bounceCurrent < bounceSequence.Length)
            StartCoroutine(Bounce(end, bounceSequence[bounceCurrent].transform.position));
        else if (bounceCurrent == bounceSequence.Length)
            StartCoroutine(Bounce(end, bounceEnding.transform.position));
        else
        {
            bounceMushroomManager.ChangeMushroomStates(bounceEndMushroomState);
            bounceActor.ClearMoveTarget(); // make the actor take it's current (end of bounce) position as it's current position
            finished = true;
        }
    }
}
