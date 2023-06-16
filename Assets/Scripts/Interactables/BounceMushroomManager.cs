using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// manages the mushroom bounce puzzle in scene 2
// Created by: Seph 5/6
// Last edit by: Seph 6/6

public enum MushroomState
{
    A,
    B,
    C,
    None
}

public class BounceMushroomManager : MonoBehaviour
{
    [SerializeField]private BounceMushroom[] mushrooms;
    [SerializeField]private bool[] mushroomStatesA;
    [SerializeField]private bool[] mushroomStatesB;
    [SerializeField]private bool[] mushroomStatesC;
    [SerializeField]private MushroomState mushroomState;
    private int mushroomCount;

    void Start()
    {
        mushroomCount = Mathf.Min(mushrooms.Length, mushroomStatesA.Length, mushroomStatesB.Length, mushroomStatesC.Length);

        if (mushroomCount != Mathf.Max(mushrooms.Length, mushroomStatesA.Length, mushroomStatesB.Length, mushroomStatesC.Length))
        {
            Debug.LogError("Mushroom manager error - mismatched array sizes");
        }

        ChangeMushroomStates(mushroomState);
    }

    private void SetMushrooms(bool[] mushroomStates)
    {
        for (int i = 0; i < mushroomCount; i++)
        {
            mushrooms[i].SetBounced(mushroomStates[i]);
        }
    }

    public void ChangeMushroomStates(MushroomState mushroomStateNew)
    {
        if (mushroomStateNew == MushroomState.None) return;

        mushroomState = mushroomStateNew;

        switch (mushroomState)
        {
            default:
            case MushroomState.A:
            {
                SetMushrooms(mushroomStatesA);
                break;
            }
            case MushroomState.B:
            {
                SetMushrooms(mushroomStatesB);
                break;
            }
            case MushroomState.C:
            {
                SetMushrooms(mushroomStatesC);
                break;
            }
        }
    }
}
