using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// manages the parallax layers
// Created by: Seph 27/5
// Last edit by: Seph 7/6

public class ParallaxManager : MonoBehaviour
{
    [SerializeField]private ActorBase actorFocus; // the initial actor that the scene should follow, typically the player
    [SerializeField]private Transform mainCamera; // the camera hold (note: not the same as the actual camera)
    [SerializeField]private Transform[] parallaxLayers; // ordered array of parallax layers
    [SerializeField]private float[] parallaxScales; // ordered array of parallax movement magnitudes
    private int layerCount;
    private Vector2 battlePos; // the position the camera should take while in battle mode
    private Vector2 battlePosPre; // the position the camera before the battle began
    private float camOrthoSize;
    private bool battleState;

    private void Awake()
    {
        layerCount = Mathf.Min(parallaxLayers.Length, parallaxScales.Length);
    }

    private void LateUpdate()
    {
        float focusX = actorFocus.transform.position.x;
        Vector3 pos;

        pos = mainCamera.position;

        if (battleState)
        {
            pos.x = battlePos.x;
            pos.y = battlePos.y;
        }
        else
        {
            pos.x = focusX;
        }

        mainCamera.position = pos;

        for (int i = 0; i < layerCount; i++)
        {
            pos = parallaxLayers[i].position;
            pos.x = focusX * parallaxScales[i];
            parallaxLayers[i].position = pos;
        }
    }

    public void SetBattlePos(Vector2 battlePosNew, float battleOrthoSize)
    {
        battlePosPre = mainCamera.position;
        battlePos = battlePosNew;
        battleState = true;
        camOrthoSize = Camera.main.orthographicSize;
        Camera.main.orthographicSize = battleOrthoSize;
    }

    public void EndBattleState()
    {
        Vector3 pos = mainCamera.position;

        pos.x = battlePosPre.x;
        pos.y = battlePosPre.y;

        mainCamera.position = pos;

        Camera.main.orthographicSize = camOrthoSize;

        battleState = false;
    }
}
