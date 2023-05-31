/****************************************************************************
 * Author:			Skylar Masson
 * Date started:	4/26/2023
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

    //Initial method that sets the instance to only this class
    void Awake() {
        if (instance == null) instance = this;
    }

    //Method at start sets player and enemy variables; disables battle-related controllers and canvas
    void Start() {
        player = GameObject.FindWithTag("Player");  //Set player
        enemy = GameObject.FindWithTag("Enemy");    //Set enemy

        player.GetComponent<PlayerController>().enabled = false;
        enemy.GetComponent<EnemyController>().enabled = false;

        //Set battle canvas inactive
        playerCanvas.SetActive(false);
        enemyCanvas.SetActive(false);

        BeginBattleEvent(callingEvent);
    }
    
    //Method that is called when the Battle begins
    //Takes in parameter to set class variable to it
    public void BeginBattleEvent(EventBattle callingEventNew) {
        callingEvent = callingEventNew;

        //Enable player and enemy battle controllers
        player.GetComponent<PlayerController>().enabled = true;
        enemy.GetComponent<EnemyController>().enabled = true;

        //Set player and enemy canvas active
        playerCanvas.SetActive(true);
        enemyCanvas.SetActive(true);

        Debug.Log("BeginBattleEvent");
    }

    public void BattleEndsVictory() {
        //Disable player and enemy battle controllers
        player.GetComponent<PlayerController>().enabled = false;
        enemy.GetComponent<EnemyController>().enabled = false;

        //Set player and enemy canvas inactive
        playerCanvas.SetActive(false);
        enemyCanvas.SetActive(false);

        Debug.Log("BattleEndsVictory");

        callingEvent.BattleEnd(true);
    }

    public void BattleEndsDefeat() {
        //Disable player and enemy battle controllers
        player.GetComponent<PlayerController>().enabled = false;
        enemy.GetComponent<EnemyController>().enabled = false;

        //Set player and enemy canvas inactive
        playerCanvas.SetActive(false);
        enemyCanvas.SetActive(false);

        Debug.Log("BattleEndsDefeat");

        callingEvent.BattleEnd(true);
    }
}
