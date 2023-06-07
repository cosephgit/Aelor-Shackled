/****************************************************************************
 * Author:			Skylar Masson
 * Date started:	4/26/2023
 * Description:  	Class contains all methods for the PlayerBattleController
 ******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleController : MonoBehaviour {

    [Header("--PUBLIC PLAYER DATA--")]
    public float fireboltVelocity, frostBeamVelocity;
    public float fireCooldown,
                 iceCooldown,
                 shieldCooldown;

    [Header("--PUBLIC PLAYER OBJECTS--")]
    public Rigidbody2D firebolt;
    public Rigidbody2D frostBiteBeam;
    public Rigidbody2D shield;
    public Rigidbody2D fireboltPosition, frostbeamPosition, shieldPosition;

    [Header("--PUBLIC SPELL UI--")]
    public Image fireImage;
    public Image iceImage;
    public Image shieldImage;

    [Header("--PUBLIC SPELLTEXT UI--")]
    public GameObject fireText;
    public GameObject frostText;
    public GameObject shieldText;

    [SerializeField] protected Animator anim;

    //Initial Method - sets above data to corresponding gameobjects
    void Awake() {
        fireImage.fillAmount = fireCooldown;
        iceImage.fillAmount = iceCooldown;
        shieldImage.fillAmount = shieldCooldown;

        fireText.SetActive(true);
        frostText.SetActive(true);
        shieldText.SetActive(true);
    }

    void Update() {
        if (fireCooldown > 0) {
            fireCooldown -= Time.deltaTime;
            fireImage.fillAmount -= 1 / (fireCooldown + 1f) * Time.deltaTime;
        } else fireText.SetActive(true);

        if (iceCooldown > 0) {
            iceCooldown -= Time.deltaTime;
            iceImage.fillAmount -= 1 / (iceCooldown + 2) * Time.deltaTime;
        } else frostText.SetActive(true);

        if (shieldCooldown > 0) {
            shieldCooldown -= Time.deltaTime;
            shieldImage.fillAmount -= 1 / (shieldCooldown + 2) * Time.deltaTime;
        } else shieldText.SetActive(true);
    }

    public void Attack(int attackNum) {
        switch (attackNum) {
            case 1: //Fire attack
                if (fireCooldown <= 0) {
                    //play animation for attack
                    Rigidbody2D newfirebolt = Instantiate(firebolt, fireboltPosition.position, transform.rotation) as Rigidbody2D;
                    newfirebolt.AddForce(transform.right * fireboltVelocity, ForceMode2D.Force);
                    fireCooldown = 3f;
                    fireImage.fillAmount = 1;
                    anim.SetTrigger("attack");
                    SoundSystemManager.instance.PlaySFX("Fire Spell Cast");
                    fireText.SetActive(false);
                }
                break;
            case 2: //Ice attack
                if (iceCooldown <= 0) {
                    //play animation for attack
                    Rigidbody2D newFrostBiteBeam = Instantiate(frostBiteBeam, frostbeamPosition.position, transform.rotation) as Rigidbody2D;
                    newFrostBiteBeam.AddForce(transform.right * frostBeamVelocity, ForceMode2D.Force);
                    iceCooldown = 5f;
                    iceImage.fillAmount = 1;
                    anim.SetTrigger("attack");
                    SoundSystemManager.instance.PlaySFX("Frost Spell Cast");
                    frostText.SetActive(false);
                }
                break;
            case 3: //Shield defense
                if (shieldCooldown <= 0) {
                    Rigidbody2D newShield = Instantiate(shield, shieldPosition.position, transform.rotation) as Rigidbody2D;
                    shieldCooldown = 5f;
                    shieldImage.fillAmount = 1;
                    Destroy(newShield, 3f);
                    anim.SetTrigger("attack");
                    SoundSystemManager.instance.PlaySFX("Defense Spell");
                    shieldText.SetActive(false);
                }
                break;
            default:
                break;
        }
    }
}
