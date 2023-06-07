using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// * Date edited:     7/6/2023 by Seph
 
public class EnemyBattleController : MonoBehaviour {

    [Header("--POWER AND ATTACK TIMING--")]
    [SerializeField]private float durationPrep = 2f;
    [SerializeField]private float durationPostLightning = 4f;
    [SerializeField]private float durationPostSuperCheck = 3f;
    [SerializeField]private float durationPostFirebolt = 2f;
    [SerializeField]private float durationShield = 2f;
    [Header("--SPELL POSITIONS--")]
    [SerializeField]private Transform powerPosition;
    [SerializeField]private Transform shieldPosition;

    [Header("--PUBLIC ENEMY OBJECTS--")]
    public Rigidbody2D firebolt;
    public Rigidbody2D lightningBolt;
    public Rigidbody2D shield;
    [SerializeField]private Vector2 lightingOffset;

    [Header("--PUBLIC ENEMY DATA--")]
    public float fireboltVelocity;
    public bool canAttack = true;
    public bool canSuperAttack = true;

    [SerializeField]private CamShake camShake;
    [SerializeField] protected Animator anim;

    HealthController health;

    //Initial Method - sets above data to corresponding gameobjects
    void Start() {
        health = GetComponent<HealthController>();
    }

    public void DetermineEnemy(int enemyNum) {
        switch (enemyNum) {
            case 1:
                StartCoroutine(Enemy1Coroutine());
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    IEnumerator Enemy1Coroutine() {
        while (canAttack) {

            yield return new WaitForSeconds(durationPrep);

            if (health.health <= 50 && canSuperAttack) {
                    anim.SetTrigger("attack");

                    // Seph: set the position of the lightning relative to the player position
                    Vector2 lightningPos = (Vector2)SceneManager.instance.playerAdventure.transform.position + lightingOffset;

                    Rigidbody2D newlightningBolt = Instantiate(lightningBolt, lightningPos, lightningBolt.transform.rotation) as Rigidbody2D;
                    SoundSystemManager.instance.PlaySFXStandard("Lightning Spell");
                    Destroy(newlightningBolt.gameObject, 1f);

                    camShake.shakeDuration = 1f;

                    canSuperAttack = false;

                    yield return new WaitForSeconds(durationPostLightning);
                }

            yield return new WaitForSeconds(durationPostSuperCheck);
            if (!canAttack) break;

            //ATTACK ONE (FIRE)
            anim.SetTrigger("attack");
            Rigidbody2D newfirebolt = Instantiate(firebolt, powerPosition.position, powerPosition.transform.rotation) as Rigidbody2D;
            newfirebolt.AddForce(-transform.right * fireboltVelocity, ForceMode2D.Force);
            SoundSystemManager.instance.PlaySFXStandard("Fire Spell Cast");
            if (!canAttack) break;

            yield return new WaitForSeconds(durationPostFirebolt);

            //ATTACK TWO (SHIELD)
            anim.SetTrigger("attack");
            Rigidbody2D newShield = Instantiate(shield, shieldPosition.position, transform.rotation) as Rigidbody2D;
            Destroy(newShield, durationShield);
        }
    }

    public void Froze() {
        StartCoroutine(Frozen());
    }

    public IEnumerator Frozen() {
        bool canSuperAttackStore = canSuperAttack;

        canSuperAttack = false;
        canAttack = false;
        yield return new WaitForSeconds(5f);
        canAttack = true;
        canSuperAttack = canSuperAttackStore;

        DetermineEnemy(1);
    }

    public void EndBattle()
    {
        StopAllCoroutines();
    }
}
