using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this manages the interaction menu pop-up
// it appears when the player clicks on an interactable object and button useability is set depending on the type of object
// it disappears when the player clicks off it, or after they click on a valid interaction option
// the interaction option is returned to the controller interface, which then passes the instruction to the interactable
// Created by: Seph 27/5
// Last edit by: Seph 28/5

public class UIInteractMenu : MonoBehaviour
{
    [SerializeField]private Button buttonLook;
    [SerializeField]private Button buttonTalk;
    [SerializeField]private Button buttonUse;
    [SerializeField]private Button buttonSpecial;


    public void OpenUIMenu(Vector3 pos, bool look, bool talk, bool use, bool special)
    {
        if (look || talk || use || special)
        {
            gameObject.SetActive(true);

            transform.position = pos;

            buttonLook.interactable = look;
            buttonTalk.interactable = talk;
            buttonUse.interactable = use;
            buttonSpecial.interactable = special;
        }
    }

    public void ButtonLookPress()
    {
        UIControlInterface.instance.SelectLook();
        gameObject.SetActive(false);
    }
    public void ButtonTalkPress()
    {
        UIControlInterface.instance.SelectTalk();
        gameObject.SetActive(false);
    }
    public void ButtonUsePress()
    {
        UIControlInterface.instance.SelectUse();
        gameObject.SetActive(false);
    }
    public void ButtonSpecialPress()
    {
        UIControlInterface.instance.SelectSpecial();
        gameObject.SetActive(false);
    }
}
