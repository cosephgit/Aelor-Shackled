using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleController : MonoBehaviour {

    [Header("--PUBLIC ENEMY OBJECTS--")]
    public Rigidbody2D firebolt;
    public Rigidbody2D powerPosition;

    [Header("--PUBLIC ENEMY DATA--")]
    public float fireboltVelocity;

    [SerializeField] protected Animator anim;

    //Initial Method - sets above data to corresponding gameobjects
    void Start() {
        //Will use later
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
        while (true) {
           // yield return new WaitForSeconds(3f);
            //attack 3 once health is below 50%
            //if (currentHealth <= 50) {
                //Debug.Log("attack3");
            //}

            yield return new WaitForSeconds(3f);
            
            //ATTACK ONE (FIRE)
            Debug.Log("attack 1");
            anim.SetTrigger("attack1");
            Rigidbody2D newfirebolt = Instantiate(firebolt, powerPosition.position, powerPosition.transform.rotation) as Rigidbody2D;
            newfirebolt.AddForce(-transform.right * fireboltVelocity, ForceMode2D.Force);
            SoundSystemManager.instance.PlaySFX("Fire Spell Cast");

            //wait another second
            yield return new WaitForSeconds(2f);
            //use attack 2
            Debug.Log("attack 2");
            //repeat
        }
    }
}
