using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLoadScene : Event
{
    [SerializeField]private int sceneIndex; // index of the scene to load

    public override void End()
    {
        base.End();
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
}
