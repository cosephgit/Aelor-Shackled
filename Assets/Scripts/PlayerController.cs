using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [Header("--PUBLIC PLAYER DATA--")]
    public float startHealth;
    public float currentHealth;

    [Header("--PUBLIC PLAYER OBJECTS--")]
    public Slider slider;

    Animator anim;

    //Initial Method - sets above data to corresponding gameobjects
    void Awake() {
        anim = GetComponent<Animator>();
        currentHealth = startHealth;
    }

    //Update Method - sets slider value to match the current health on a constant basis
    void Update() {
        slider.value = currentHealth;
    }

    public void FireAttack() {
        //play animation for attack
    }

    public void IceAttack() {

    }
}
