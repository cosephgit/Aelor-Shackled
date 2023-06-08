using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField]private Button buttonQuit;
    [SerializeField]private AudioClip menuButton;
    [SerializeField]private AudioClip menuMusic;

    private void Start()
    {
        SoundSystemManager.instance.PlayMusic(menuMusic);
    }

    public void ButtonPlay()
    {
        SoundSystemManager.instance.PlaySFXStandard(menuButton);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ButtonQuit()
    {
        SoundSystemManager.instance.PlaySFXStandard(menuButton);
        #if UNITY_EDITOR
        Debug.Log("QUIT");
        #else
        Application.Quit();
        #endif
    }
}
