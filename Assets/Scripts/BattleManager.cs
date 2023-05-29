using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    public static BattleManager instance;

    EventBattle callingEvent;

    [Header("--PUBLIC GAMEOBJECTS--")]
    public GameObject player;
    public GameObject enemy;

    [Header("--PUBLIC CANVAS--")]
    public GameObject playerCanvas;
    public GameObject enemyCanvas;

    void Awake() {
        if (instance == null) instance = this;
    }

    void Start() {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");

        player.GetComponent<PlayerController>().enabled = false;
        enemy.GetComponent<EnemyController>().enabled = false;

        playerCanvas.SetActive(false);
        enemyCanvas.SetActive(false);

        BeginBattleEvent(callingEvent);
    }
    
    public void BeginBattleEvent(EventBattle callingEventNew) {
        callingEvent = callingEventNew;
        player.GetComponent<PlayerController>().enabled = true;
        enemy.GetComponent<EnemyController>().enabled = true;

        playerCanvas.SetActive(true);
        enemyCanvas.SetActive(true);

        Debug.Log("BeginBattleEvent");
    }

    public void BattleEndsVictory() {
        player.GetComponent<PlayerController>().enabled = false;
        enemy.GetComponent<EnemyController>().enabled = false;

        playerCanvas.SetActive(false);
        enemyCanvas.SetActive(false);

        Debug.Log("BattleEndsVictory");

        callingEvent.BattleEnd(true);
    }

    public void BattleEndsDefeat() {
        player.GetComponent<PlayerController>().enabled = false;
        enemy.GetComponent<EnemyController>().enabled = false;

        playerCanvas.SetActive(false);
        enemyCanvas.SetActive(false);

        Debug.Log("BattleEndsDefeat");

        callingEvent.BattleEnd(false);
    }
}
