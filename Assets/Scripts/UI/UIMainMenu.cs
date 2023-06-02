using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField]private Button buttonQuit;

    public void ButtonPlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ButtonQuit()
    {
        #if UNITY_EDITOR
        Debug.Log("QUIT");
        #else
        Application.Quit();
        #endif
    }
}
