using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// global static class for storing constants

public static class Global
{
    public const string LAYERMOVEAREA = "MoveBoundary";
    public const string LAYERINTERACTABLE = "Interactable";

    public static LayerMask LayerMove()
    {
        return LayerMask.GetMask(LAYERMOVEAREA);
    }
    public static LayerMask LayerInteract()
    {
        return LayerMask.GetMask(LAYERINTERACTABLE);
    }
}
