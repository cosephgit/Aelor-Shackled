using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [Header("Menu pages")]
    [SerializeField]private GameObject menuMain;
    [SerializeField]private GameObject menuOptions;
    [Header("UI elements")]
    [SerializeField]private Button buttonQuit;
    [SerializeField]private AudioClip menuButton;
    [SerializeField]private AudioClip menuMusic;
    [SerializeField]private Slider sliderVolumeMusic;
    [SerializeField]private Slider sliderVolumeEffects;
    [SerializeField]private Slider sliderDialogue;
    [SerializeField]private Toggle toggleAdventureMode;

    private void Start()
    {
        SoundSystemManager.instance.PlayMusic(menuMusic);
        sliderVolumeMusic.value = GameManager.instance.volumeMusic;
        sliderVolumeEffects.value = GameManager.instance.volumeEffect;
        sliderDialogue.value = GameManager.instance.dialogueSpeed;
        toggleAdventureMode.isOn = GameManager.instance.adventureMode;
    }

    public void ButtonPlay()
    {
        SoundSystemManager.instance.PlaySFXStandard(menuButton);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ButtonOptions()
    {
        SoundSystemManager.instance.PlaySFXStandard(menuButton);
        menuMain.SetActive(false);
        menuOptions.SetActive(true);
    }

    public void ButtonReturn()
    {
        SoundSystemManager.instance.PlaySFXStandard(menuButton);
        menuMain.SetActive(true);
        menuOptions.SetActive(false);
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

    public void SliderMusic(System.Single volume)
    {
        GameManager.instance.SetVolumeMusic(volume);
    }
    public void SliderEffects(System.Single volume)
    {
        GameManager.instance.SetVolumeEffect(volume);
    }
    public void SliderDialogue(System.Single speed)
    {
        GameManager.instance.SetDialogueSpeed(speed);
    }
    public void ToggleAdventureMode(System.Boolean enabled)
    {
        GameManager.instance.SetAdventureMode(enabled);
    }
}
