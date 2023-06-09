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

    [Header("--POWER SETTINGS--")]
    [SerializeField]private float fireboltVelocity = 150f;
    [SerializeField]private float shieldDuration = 2f;
    [SerializeField]private float frozenDuration = 2f;

    [Header("--POWER OBJECTS AND REFERENCES--")]
    [SerializeField]private Transform powerPosition;
    [SerializeField]private Transform shieldPosition;
    public Rigidbody2D firebolt;
    public Rigidbody2D lightningBolt;
    public Rigidbody2D shield;
    [SerializeField]private Vector2 lightingOffset;

    [Header("--MISC REFERENCES--")]
    [SerializeField]private CamShake camShake;
    [SerializeField]protected Animator anim;

    private HealthController health;
    private bool canAttack = true;
    private bool canSuperAttack = true;
    private int enemyIndex; // the stored index of this enemy

    //Initial Method - sets above data to corresponding gameobjects
    void Start() {
        health = GetComponent<HealthController>();
    }

    public void DetermineEnemy(int enemyNum) {
        enemyIndex = enemyNum;
    }

    private void StartAttack()
    {
        switch (enemyIndex) {
            case 1: // first sorcerer
                StartCoroutine(Enemy1Coroutine());
                break;
            case 2: // mushroom sorcerer
                break;
            case 3: // snake monster
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

            if (!canAttack) break;

            yield return new WaitForSeconds(cooldownFirebolt);

            //ATTACK ONE (FIRE)
            SpellFirebolt();

            if (!canAttack) break;

            yield return new WaitForSeconds(cooldownFirebolt);

            //ATTACK TWO (SHIELD)
            SpellShield();

            yield return new WaitForSeconds(cooldownShield);
        }
    }

    // called when this enemy is hit by the player's freeze ray
    public void Froze() {
        StartCoroutine(Frozen());
    }

    public IEnumerator Frozen() {
        StopAllCoroutines();

        yield return new WaitForSeconds(frozenDuration);

        StartAttack();
    }

    public void EndBattle()
    {
        StopAllCoroutines();
    }
}
