using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the scene manager is a singleton unique to each scene
// it acts as a service provider for key references to e.g. the player pawn, the movement area collider(s), and any scripts that should be run on scene start

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;
    [field: SerializeField]public Collider2D moveArea { get; private set; } // the collider which defines the area which the player pawn can move within
    [field: SerializeField]public PlayerAdventureController playerAdventure { get; private set; } // the player controller during adventure mode

    private void Awake()
    {
        if (instance)
        {
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        else
            instance = this;
    }
}
