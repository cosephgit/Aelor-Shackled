using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// this low-level base class is used to make an object play dialogue lines
// all interactables and anything else that can act in the world are based on this
// it doesn't have to have a text renderer! if it doesn't have one it simply wont play dialogue

public class TalkableBase : MonoBehaviour
{
    [SerializeField]private TextMeshPro text;

    protected virtual void Awake()
    {
        if (text)
        {
            text.enabled = false;
        }
    }

    public void ShowLine(string line)
    {
        if (text)
        {
            text.enabled = true;
            text.text = line;
        }
    }
    public void HideLine()
    {
        if (text)
        {
            text.enabled = false;
        }
    }
}
