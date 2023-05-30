using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [SerializeField]private ActorBase actorFocus; // the initial actor that the scene should follow, typically the player
    [SerializeField]private Camera mainCamera;
    [SerializeField]private Transform[] parallaxLayers; // ordered array of parallax layers
    [SerializeField]private float[] parallaxScales; // ordered array of parallax movement magnitudes
    private int layerCount;

    private void Awake()
    {
        layerCount = Mathf.Min(parallaxLayers.Length, parallaxScales.Length);
    }

    private void LateUpdate()
    {
        float focusX = actorFocus.transform.position.x;
        Vector3 pos;

        pos = mainCamera.transform.position;
        pos.x = focusX;
        mainCamera.transform.position = pos;

        for (int i = 0; i < layerCount; i++)
        {
            pos = parallaxLayers[i].position;
            pos.x = focusX * parallaxScales[i];
            parallaxLayers[i].position = pos;
        }
    }
}
