using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// global static class for storing constants
// Created by: Seph 27/5
// Last edit by: Seph 28/5

public static class Global
{
    // dialogue constants
    public const float DIALOGUESLOWDOWN = 0.25f; // normal dialogue speed
    public const float DIALOGUESPEEDBOOST = 1.75f; // extra dialogue speed when mouse is held
    // gameplay constants
    public const int INVENTORYSLOTS = 6; // the number of inventory slots in the game
    // layer names
    public const string LAYERMOVEAREA = "MoveBoundary";
    public const string LAYERINTERACTABLE = "Interactable";
    // save data settings
    public const string SAVEVOLUMEMUSIC = "VolumeMusic"; // float
    public const string SAVEVOLUMEEFFECT = "VolumeEffect"; // float
    public const string SAVEDIALOGUESPEED = "Dialogue"; // float
    public const string SAVEADVENTUREMODE = "AdventureMode"; // int
    public const string SAVEPROGRESS = "SceneReached"; // int

    public static LayerMask LayerMove()
    {
        return LayerMask.GetMask(LAYERMOVEAREA);
    }
    public static LayerMask LayerInteract()
    {
        return LayerMask.GetMask(LAYERINTERACTABLE);
    }

    public static float VolToDecibels(float vol)
    {
        float decibels;
        if (vol < 0.01f)
        {
            // can't do log 0
            decibels = -80f;
        }
        else
        {
            decibels = Mathf.Log(vol, 2f); // so each halving of volume is -1
            decibels *= 10f; // -10 decibels is approximately half volume
        }
        return decibels;
    }
}
