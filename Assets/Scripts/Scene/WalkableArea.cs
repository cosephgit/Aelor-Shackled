using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages a navigation collider
// contains connection data to other navigation colliders in the scene for pathfinding between movement areas
// Created by: Seph 29/5
// Last edit by: Seph 29/5

public class WalkableArea : MonoBehaviour
{
    [field: SerializeField]public Collider2D walkCollider { get; private set; }
    [SerializeField]private SpriteRenderer sprite;
    [SerializeField]private Transform[] walkConnection; // a point connecting to another walkable area
    [SerializeField]private WalkableArea[] walkArea; // a connected walkable area
    private int connectionCount;

    private void Awake()
    {
        connectionCount = Mathf.Min(walkConnection.Length, walkArea.Length);

        if (connectionCount != Mathf.Max(walkArea.Length, walkConnection.Length))
            Debug.LogError("connection and area count and mismatch");
    }

    void Start()
    {
        if (!SceneManager.instance.DEBUG)
            sprite.color = Color.clear; // hide the debug sprite
    }

    // looks for the passed walkable area reference in all connections
    // this is a very quick and dirty pathfinding algorithm
    // in the event of a system with loops it will not reliably produce a shortest path, it only works consistently with branches that do not reconnect
    public bool FindConnection(List<WalkableArea> origin, WalkableArea target, out Vector2 movePoint, out WalkableArea moveArea)
    {
        origin.Add(this);

        for (int i = 0; i < connectionCount; i++)
        {
            if (walkArea[i] == target)
            {
                // this node does provide a connection to the target
                movePoint = walkConnection[i].position;
                moveArea = walkArea[i];
                return true;
            }
            // this protects from infinite loops in the event of a circular path, which you shouldn't have added to the game anyway but let's be sure we avoid an infinite loop
            if (origin.Contains(walkArea[i])) continue; // don't check a node that's already been checked
            // check for a connection on the next node
            // if it's not null then we've found a valid connection, we return THIS connection only
            if (walkArea[i].FindConnection(origin, target, out movePoint, out moveArea))
            {
                movePoint = walkConnection[i].position;
                moveArea = walkArea[i];
                return true;
            }
        }

        movePoint = Vector2.zero;
        moveArea = null;
        return false; // there is no connection, pathfinding failed to this node
    }

    // an ActorBase calls this when it enters this walkable area
    public int GetAreaLayer()
    {
        return sprite.sortingLayerID;
    }
}
