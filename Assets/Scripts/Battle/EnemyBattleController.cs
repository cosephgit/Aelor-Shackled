using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// * Date edited:     9/6/2023 by Seph
 
public class EnemyBattleController : MonoBehaviour {

    [Header("--POWER AND ATTACK TIMING--")]
    [SerializeField]private float timePreAttack = 1f;
    [SerializeField]private float cooldownLightning = 4f;
    [SerializeField]private float cooldownFirebolt = 2f;
    [SerializeField]private float cooldownShield = 2.5f;
    [SerializeField]private float cooldownSpores = 1f;
    [SerializeField]private float cooldownVenom = 1f;


    [Header("--POWER SETTINGS--")]
    [SerializeField]private float fireboltVelocity = 150f;
    [SerializeField]private float shieldDuration = 2f;
    [SerializeField]private float frozenDuration = 2f;
    [SerializeField]private float sporesDuration = 4f;
    [SerializeField]private float venomRepeatDelay = 1f;

    [Header("--POWER OBJECTS AND REFERENCES--")]
    [SerializeField]private Transform powerPosition;
    [SerializeField]private Transform shieldPosition;
    public Rigidbody2D firebolt;
    public Rigidbody2D lightningBolt;
    public Rigidbody2D shield;
    [SerializeField]private Vector2 lightingOffset;
    [SerializeField]private Rigidbody2D spores;
    [SerializeField]private Rigidbody2D venom;

    [Header("--MISC REFERENCES--")]
    [SerializeField]private CamShake camShake;
    [SerializeField]protected Animator anim;
    [SerializeField]private AudioClip sporeSound;
    [SerializeField]private AudioClip venomSound;

    private HealthController health;
    private bool canAttack = true;
    private bool canSuperAttack = true;
    private int enemyIndex; // the stored index of this enemy

    //Initial Method - sets above data to corresponding gameobjects
    void Awake() {
        health = GetComponent<HealthController>();
    }

    public void DetermineEnemy(int enemyNum) {
        enemyIndex = enemyNum;
        health.Initialise();
        canAttack = true;
        canSuperAttack = true;
        StartAttack();
    }

    private void StartAttack()
    {
        switch (enemyIndex) {
            case 1: // first sorcerer
                StartCoroutine(Enemy1Coroutine());
                break;
            case 2: // mushroom sorcerer
                StartCoroutine(Enemy2Coroutine());
                break;
            case 3: // snake monster
                StartCoroutine(Enemy3Coroutine());
                break;
            default:
                break;
        }
    }

    private void SpellLightning()
    {
        anim.SetTrigger("attack");

        // Seph: set the position of the lightning relative to the player position
        Vector2 lightningPos = (Vector2)SceneManager.instance.playerAdventure.transform.position + lightingOffset;

        Rigidbody2D newlightningBolt = Instantiate(lightningBolt, lightningPos, lightningBolt.transform.rotation) as Rigidbody2D;
        Destroy(newlightningBolt.gameObject, 1f);

        SoundSystemManager.instance.PlaySFXStandard("Lightning Spell");

        camShake.shakeDuration = 1f;
    }

    private void SpellFirebolt()
    {
        anim.SetTrigger("attack");
        Rigidbody2D newfirebolt = Instantiate(firebolt, powerPosition.position, powerPosition.transform.rotation) as Rigidbody2D;
        newfirebolt.AddForce(-transform.right * fireboltVelocity, ForceMode2D.Force);
        SoundSystemManager.instance.PlaySFXStandard("Fire Spell Cast");
    }

    private void SpellShield()
    {
        anim.SetTrigger("attack");
        Rigidbody2D newShield = Instantiate(shield, shieldPosition.position, transform.rotation) as Rigidbody2D;
        Destroy(newShield.gameObject, shieldDuration);
    }

    // launch the spores attack
    private void SpellSpores()
    {
        anim.SetTrigger("attack");
        Rigidbody2D newSpores = Instantiate(spores, SceneManager.instance.playerAdventure.transform.position, transform.rotation) as Rigidbody2D;
        TimedEffect[] sporeEffects = newSpores.GetComponentsInChildren<TimedEffect>();

        for (int i = 0; i < sporeEffects.Length; i++)
            sporeEffects[i].PlayEffects();

        Destroy(newSpores.gameObject, sporesDuration);

        SoundSystemManager.instance.PlaySFXStandard(sporeSound);

        BattleManager.instance.playerBattleController.SporeHit();
    }

    // launch the snake venom attack
    private void SpellVenom()
    {
        anim.SetTrigger("attack");
        Rigidbody2D newVenom = Instantiate(venom, powerPosition.position, powerPosition.transform.rotation) as Rigidbody2D;
        newVenom.AddForce(-transform.right * fireboltVelocity, ForceMode2D.Force);

        SoundSystemManager.instance.PlaySFXStandard(venomSound);

        BattleManager.instance.playerBattleController.SporeHit();
    }

    IEnumerator Enemy1Coroutine() {
        while (canAttack) {

            yield return new WaitForSeconds(timePreAttack);

            if (health.health <= 50 && canSuperAttack)
            {
                SpellLightning();
                canSuperAttack = false;
                yield return new WaitForSeconds(cooldownLightning);
            }

            if (!canAttack) break;

            //ATTACK ONE (FIRE)
            SpellFirebolt();

            yield return new WaitForSeconds(cooldownFirebolt);

            if (!canAttack) break;

            //ATTACK TWO (SHIELD)
            SpellShield();

            yield return new WaitForSeconds(cooldownShield);

            if (!canAttack) break;

            //ATTACK ONE (FIRE)
            SpellFirebolt();

            yield return new WaitForSeconds(cooldownFirebolt);
        }
    }

    // coroutine for the second boss: mushroomer sorcerer
    IEnumerator Enemy2Coroutine() {
        while (canAttack) {

            yield return new WaitForSeconds(timePreAttack);

            if (health.health <= 50 && canSuperAttack)
            {
                SpellLightning();
                canSuperAttack = false;
                yield return new WaitForSeconds(cooldownLightning);
            }

            if (!canAttack) break;

            SpellSpores();

            yield return new WaitForSeconds(cooldownSpores);

            if (!canAttack) break;

            //ATTACK ONE (FIRE)
            SpellFirebolt();

            yield return new WaitForSeconds(cooldownFirebolt);

            if (!canAttack) break;

            //ATTACK TWO (SHIELD)
            SpellShield();

            yield return new WaitForSeconds(cooldownShield);

            if (!canAttack) break;

            //ATTACK ONE (FIRE)
            SpellFirebolt();

            yield return new WaitForSeconds(cooldownFirebolt);
        }
    }

    // coroutine for the final boss - magic snake
    IEnumerator Enemy3Coroutine() {
        while (canAttack) {

            yield return new WaitForSeconds(timePreAttack);

            if (health.health <= 100 && canSuperAttack)
            {
                SpellVenom();
                yield return new WaitForSeconds(venomRepeatDelay);
                SpellVenom();
                yield return new WaitForSeconds(venomRepeatDelay);
                SpellVenom();
                canSuperAttack = false;
                yield return new WaitForSeconds(cooldownVenom);
            }

            if (!canAttack) break;

            //ATTACK ONE (FIRE)
            SpellFirebolt();

            yield return new WaitForSeconds(cooldownFirebolt);

            if (!canAttack) break;

            //ATTACK TWO (SHIELD)
            SpellShield();

            yield return new WaitForSeconds(cooldownShield);

            if (!canAttack) break;

            //ATTACK ONE (FIRE)
            SpellFirebolt();

            yield return new WaitForSeconds(cooldownFirebolt);

            if (!canAttack) break;

            //ATTACK TWO (SHIELD)
            SpellShield();

            yield return new WaitForSeconds(cooldownShield);

            if (!canAttack) break;

            // ATTACK SUPER - BOSS 3 DOES IT EVERY LOOP!
            SpellLightning();

            yield return new WaitForSeconds(cooldownLightning);
        }
    }


    // called when this enemy is hit by the player's freeze ray
    public void Froze() {
        StopAllCoroutines();
        StartCoroutine(Frozen());
    }

    public IEnumerator Frozen() {
        yield return new WaitForSeconds(frozenDuration);

        StartAttack();
    }

    public void EndBattle()
    {
        StopAllCoroutines();
        canAttack = false;
        canSuperAttack = false;
    }
}
