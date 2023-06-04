using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleController : MonoBehaviour {

    [Header("--PUBLIC ENEMY OBJECTS--")]
    public Rigidbody2D firebolt;
    public Rigidbody2D lightningBolt;
    public Rigidbody2D powerPosition, lightningPosition;

    [Header("--PUBLIC ENEMY DATA--")]
    public float fireboltVelocity;
    public bool canAttack;
    public bool canSuperAttack;

    [SerializeField] Camera cam;
    [SerializeField] protected Animator anim;

    HealthController health;

    //Initial Method - sets above data to corresponding gameobjects
    void Start() {
        canAttack = true;
        canSuperAttack = true;
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
            yield return new WaitForSeconds(2f);
            if (health.health <= 50 && canSuperAttack) {
                    anim.SetTrigger("attack1");
                    Rigidbody2D newlightningBolt = Instantiate(lightningBolt, lightningPosition.position, lightningPosition.transform.rotation) as Rigidbody2D;
                    SoundSystemManager.instance.PlaySFX("Lightning Spell");
                    cam.GetComponent<CamShake>().shakeDuration = 1f;
                    Destroy(newlightningBolt, 1.5f);
                    yield return new WaitForSeconds(4f);
                    canSuperAttack = false;
                }

            yield return new WaitForSeconds(3f);
            if (!canAttack) break;

            //ATTACK ONE (FIRE)
            Debug.Log("attack 1");
            anim.SetTrigger("attack1");
            Rigidbody2D newfirebolt = Instantiate(firebolt, powerPosition.position, powerPosition.transform.rotation) as Rigidbody2D;
            newfirebolt.AddForce(-transform.right * fireboltVelocity, ForceMode2D.Force);
            SoundSystemManager.instance.PlaySFX("Fire Spell Cast");
            if (!canAttack) break;
        }
    }

    public void Froze() {
        StartCoroutine(Frozen());
    }

    public IEnumerator Frozen() {
        canAttack = false;
        yield return new WaitForSeconds(5f);
        canAttack = true;
        DetermineEnemy(1);
    }
}
