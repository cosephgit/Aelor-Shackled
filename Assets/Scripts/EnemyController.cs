using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    [Header("--PUBLIC ENEMY DATA--")]
    public float startHealth;
    public float currentHealth;

    [Header("--PUBLIC ENEMY OBJECTS--")]
    public Slider slider;

    Animator anim;

    //Initial Method - sets above data to corresponding gameobjects
    void Awake() {
        anim = GetComponent<Animator>();
        currentHealth = startHealth;

        //BattleController calls this method instead
        DetermineEnemy(1);
    }

    void DetermineEnemy(int enemyNum) {
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
            yield return new WaitForSeconds(3f);
            //attack 3 once health is below 50%
            if (currentHealth <= 50) {
                Debug.Log("attack3");
            }

            yield return new WaitForSeconds(3f);
            //use attack 1
            Debug.Log("attack 1");
            //wait another second
            yield return new WaitForSeconds(2f);
            //use attack 2
            Debug.Log("attack 2");
            //repeat
        }
    }
}
