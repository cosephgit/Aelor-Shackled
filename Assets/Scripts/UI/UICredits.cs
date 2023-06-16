using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICredits : MonoBehaviour
{
    [SerializeField]private AudioClip menuButton;
    [SerializeField]private AudioClip menuMusic;

    private void Start()
    {
        SoundSystemManager.instance.PlayMusic(menuMusic);
    }

    public void ButtonMenu()
    {
        SoundSystemManager.instance.PlaySFXStandard(menuButton);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
