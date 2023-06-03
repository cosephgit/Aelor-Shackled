/****************************************************************************
 * Author:			Skylar Masson
 * Date started:	4/26/2023
 * Date edited:     2/6/2023 (Seph)
 * Description:  	Class contains all methods for the Battle Sequence
 ******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is for managing the battle sequence
public class BattleManager : MonoBehaviour {

    public static BattleManager instance;   //Creating a singleton so each script can call it without having to assign it to each class that uses it.

    [SerializeField] EventBattle callingEvent;  //Store the current battle even

    [Header("--PUBLIC GAMEOBJECTS--")]  //All public Gameobjects
    public GameObject player;
    public GameObject enemy;

    [Header("--PUBLIC CANVAS--")]       //All public canvas's
    public GameObject playerCanvas;
    public GameObject enemyCanvas;

    private PlayerBattleController playerController;
    private EnemyBattleController enemyController;

    //Initial method that sets the instance to only this class
    void Awake() {
        if (instance == null) instance = this;
    }

    //Method at start sets player and enemy variables; disables battle-related controllers and canvas
    void Start() {

        if (!player)
            player = GameObject.FindWithTag("Player");  //Set player
        playerController = player.GetComponent<PlayerBattleController>();

        if (!enemy)
            enemy = GameObject.FindWithTag("Enemy");    //Set enemy
        enemyController = enemy.GetComponent<EnemyBattleController>();

        playerController.enabled = false;
        enemyController.enabled = false;

        //Set battle canvas inactive
        playerCanvas.gameObject.SetActive(false);
        enemyCanvas.gameObject.SetActive(false);
    }

    //Method that is called when the Battle begins
    //Takes in parameter to set class variable to it
    public void BeginBattleEvent(EventBattle callingEventNew) {
        callingEvent = callingEventNew;

        //Enable player and enemy battle controllers
        playerController.enabled = true;
        enemyController.enabled = true;

        //Set player and enemy canvas active
        playerCanvas.SetActive(true);
        enemyCanvas.SetActive(true);

        #if UNITY_EDITOR
        Debug.Log("BeginBattleEvent");
        #endif
    }

    public void BattleEndsVictory() {
        //Disable player and enemy battle controllers
        playerController.enabled = false;
        enemyController.enabled = false;

        //Set player and enemy canvas inactive
        playerCanvas.SetActive(false);
        enemyCanvas.SetActive(false);

        #if UNITY_EDITOR
        Debug.Log("BattleEndsVictory");
        #endif

        callingEvent.BattleEnd(true);
    }

    public void BattleEndsDefeat() {
        //Disable player and enemy battle controllers
        playerController.enabled = false;
        enemyController.enabled = false;

        //Set player and enemy canvas inactive
        playerCanvas.SetActive(false);
        enemyCanvas.SetActive(false);

        #if UNITY_EDITOR
        Debug.Log("BattleEndsDefeat");
        #endif

        callingEvent.BattleEnd(true);
    }
}
