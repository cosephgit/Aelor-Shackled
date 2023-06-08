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
    [SerializeField]private float cameraSpeed = 8f; // how quickly the camera moves
    [SerializeField]private float cameraSpeedOrtho = 1f; // how quickly the camera's ortho size changes
    private int layerCount;
    private Vector2 lockPoint; // the position the camera should take while in battle mode
    private Vector2 lockPointPre; // the position the camera before the battle began
    private float camOrthoSizeOriginal;
    private float camOrthoSizeTarget;
    private bool lockState;
    private Vector3 cameraTarget;

    private void Awake()
    {
        layerCount = Mathf.Min(parallaxLayers.Length, parallaxScales.Length);
        camOrthoSizeOriginal = Camera.main.orthographicSize;
        camOrthoSizeTarget = camOrthoSizeOriginal;
    }

    private void LateUpdate()
    {
        cameraTarget = mainCamera.position;

        if (lockState)
        {
            cameraTarget.x = lockPoint.x;
            cameraTarget.y = lockPoint.y;
        }
        else
        {
            cameraTarget.x = actorFocus.transform.position.x;
        }

        Vector3 camOffset = cameraTarget - mainCamera.position;
        float camOrthoOffset = camOrthoSizeTarget - Camera.main.orthographicSize;

        if (!Mathf.Approximately(camOffset.magnitude, 0))
        {
            float cameraMoveFrame = cameraSpeed * Time.deltaTime;

            if (camOffset.magnitude > cameraMoveFrame)
                camOffset = camOffset.normalized * cameraMoveFrame;

            mainCamera.position = mainCamera.position + camOffset;
        }

        if (!Mathf.Approximately(camOrthoOffset, 0f))
        {
            float camOrthoChangeFrame = cameraSpeedOrtho * Time.deltaTime;

            if (Mathf.Abs(camOrthoOffset) > camOrthoChangeFrame)
            {
                if (camOrthoOffset > 0)
                    camOrthoOffset = camOrthoChangeFrame;
                else
                    camOrthoOffset = -camOrthoChangeFrame;
            }

            Camera.main.orthographicSize += camOrthoOffset;
        }

        for (int i = 0; i < layerCount; i++)
        {
            Vector2 pos = parallaxLayers[i].position;
            pos.x = mainCamera.position.x * parallaxScales[i];
            parallaxLayers[i].position = pos;
        }
    }

    public void SetCameraLock(Vector2 lockPointSet, float orthoSize = 0)
    {
        lockPointPre = mainCamera.position;
        lockPoint = lockPointSet;
        lockState = true;
        if (orthoSize > 0)
            camOrthoSizeTarget = orthoSize;
    }

    public void EndCameraLock()
    {
        camOrthoSizeTarget = camOrthoSizeOriginal;

        // restore the original camera y offset
        cameraTarget.y = lockPointPre.y;

        lockState = false;
    }
}
