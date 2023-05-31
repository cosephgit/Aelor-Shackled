using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleController : MonoBehaviour {

    [Header("--PUBLIC PLAYER DATA--")]
    public float fireboltVelocity, frostBeamVelocity;
    public float fireCooldown,
                 iceCooldown;

    [Header("--PUBLIC PLAYER OBJECTS--")]
    public Rigidbody2D firebolt;
    public Rigidbody2D frostBiteBeam;
    public Rigidbody2D staffPosition;

    [Header("--PUBLIC SPELL UI--")]
    public Image fireImage;
    public Image iceImage;


    Animator anim;

    //Initial Method - sets above data to corresponding gameobjects
    void Awake() {
        anim = GetComponent<Animator>();
        fireImage.fillAmount = fireCooldown;
        iceImage.fillAmount = iceCooldown;
    }

    void Update() {
        if (fireCooldown > 0) {
            fireCooldown -= Time.deltaTime;
            fireImage.fillAmount -= 1 / (fireCooldown + 5) * Time.deltaTime;
        }

        if (iceCooldown > 0) {
            iceCooldown -= Time.deltaTime;
            iceImage.fillAmount -= 1 / (iceCooldown + 5) * Time.deltaTime;
        }
    }

    public void Attack(int attackNum) {
        switch (attackNum) {
            case 1: //Fire attack
                if (fireCooldown <= 0) {
                    //play animation for attack
                    Rigidbody2D newfirebolt = Instantiate(firebolt, staffPosition.position, transform.rotation) as Rigidbody2D;
                    newfirebolt.AddForce(transform.right * fireboltVelocity, ForceMode2D.Force);
                    fireCooldown = 10f;
                    fireImage.fillAmount = 1;
                }
                break;
            case 2: //Ice attack
                if (iceCooldown <= 0) {
                    //play animation for attack
                    Rigidbody2D newFrostBiteBeam = Instantiate(frostBiteBeam, staffPosition.position, transform.rotation) as Rigidbody2D;
                    newFrostBiteBeam.AddForce(transform.right * frostBeamVelocity, ForceMode2D.Force);
                    iceCooldown = 10f;
                    iceImage.fillAmount = 1;
                }
                break;
            case 3: //Shield defense
                break;
            default:
                break;
        }
    }
}
