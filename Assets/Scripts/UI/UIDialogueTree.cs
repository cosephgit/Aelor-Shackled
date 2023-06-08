using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// manages the dialogue tree UI menu
// Created by: Seph 28/5
// Last edit by: Seph 1/6

public class UIDialogueTree : MonoBehaviour
{
    [Header("Ordered array of buttons")]
    [SerializeField]private Button[] buttons;
    [SerializeField]private GameObject buttonHolder;
    [SerializeField]private AudioClip soundButton;
    [SerializeField]private AudioClip soundInteract;
    private TextMeshProUGUI[] buttonTexts;
    private int buttonCount;
    private Event currentDialogueTree;

    private void Awake()
    {
        buttonCount = buttons.Length;
        buttonTexts = new TextMeshProUGUI[buttonCount];

        for (int i = 0; i < buttonCount; i++)
        {
            buttonTexts[i] = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
        }

        gameObject.SetActive(false);
    }

    // opens the dialogue tree with the provided dialogue lines
    public void OpenDialogue(Vector3 pos, string[] lines, Event parentEvent)
    {
        gameObject.SetActive(true);

        SoundSystemManager.instance.PlaySFXStandard(soundInteract);

        currentDialogueTree = parentEvent;

        buttonHolder.transform.position = UIControlInterface.instance.WorldToScreenPos(pos);

        // show option buttons for only the provided lines
        for (int i = 0; i < buttonCount; i++)
        {
            if (i < lines.Length)
            {
                buttons[i].gameObject.SetActive(true);
                buttonTexts[i].text = lines[i];
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }

    // button for dialogue option pressed with indicated index
    public void ButtonDialogue(int index)
    {
        currentDialogueTree.EndEventRemote(index);

        SoundSystemManager.instance.PlaySFXStandard(soundButton);

        Debug.Log("dialogue button " + index + " pressed");

        gameObject.SetActive(false);
    }
}
