using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    public static BattleManager instance;   //Creating a singleton instance

    public bool isBattleTime;

    [Header("--PUBLIC GAMEOBJECTS--")]
    public GameObject player;
    public GameObject enemy;

    [Header("--PUBLIC CANVAS--")]
    public GameObject playerCanvas;
    public GameObject enemyCanvas;

    void Start() {
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.FindWithTag("Enemy");

        player.GetComponent<PlayerController>().enabled = false;
        enemy.GetComponent<EnemyController>().enabled = false;

        playerCanvas.SetActive(false);
        enemyCanvas.SetActive(false);
    }
    
    public void BeginBattleEvent() {
        isBattleTime = true;

        player.GetComponent<PlayerController>().enabled = true;
        enemy.GetComponent<EnemyController>().enabled = true;

        playerCanvas.SetActive(true);
        enemyCanvas.SetActive(true);
    }
}
