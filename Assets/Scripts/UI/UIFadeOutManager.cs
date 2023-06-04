using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this manages the UI fade out elements
// Created by: Seph 4/6
// Last edit by: Seph 4/6

public class UIFadeOutManager : MonoBehaviour
{
    [SerializeField]private Image fadeMain;
    [SerializeField]private float fadeTimeFull = 1f; // the time it takes to transition from 0 to 1 (or 1 to 0)
    float fadeCurrent = 0f;

    // this sets the fade target of the manager, so that it gradually transitions to a full fade out
    public void SetFadeTarget(float target)
    {
        float targetClamp = Mathf.Clamp(target, 0f, 1f);

        if (targetClamp != fadeCurrent)
        {
            StopAllCoroutines();

            StartCoroutine(FadeTransition(target));
        }
    }

    // this forces the fade out level to the provided value immediately
    public void SeFadeDirect(float target)
    {
        fadeCurrent = target;
        UpdateFadeShow();
    }

    // continues running until the current fade level reaches the provided target fade level
    private IEnumerator FadeTransition(float target)
    {
        while (fadeCurrent != target)
        {
            if (target > fadeCurrent)
                fadeCurrent = Mathf.Min(fadeCurrent + Time.deltaTime / fadeTimeFull, target);
            else
                fadeCurrent = Mathf.Max(fadeCurrent - Time.deltaTime / fadeTimeFull, target);
            UpdateFadeShow();
            yield return new WaitForEndOfFrame();
        }
    }

    private void UpdateFadeShow()
    {
        Color colorNew = fadeMain.color;

        colorNew.a = fadeCurrent;

        fadeMain.color = colorNew;
    }
}
