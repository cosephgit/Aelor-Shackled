using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("--PUBLIC PLAYER DATA--")]
    public float fireboltVelocity;

    [Header("--PUBLIC PLAYER OBJECTS--")]
    public Rigidbody2D firebolt;
    public Rigidbody2D staffPosition;

    Animator anim;

    //Initial Method - sets above data to corresponding gameobjects
    void Awake() {
        anim = GetComponent<Animator>();
    }

    public void FireAttack() {
        //play animation for attack
        Debug.Log("FIRE ATTACK");
        Rigidbody2D newfirebolt = Instantiate(firebolt, staffPosition.position, transform.rotation) as Rigidbody2D;
		newfirebolt.AddForce(transform.right * fireboltVelocity, ForceMode2D.Force);
    }

    public void IceAttack() {

    }
}
