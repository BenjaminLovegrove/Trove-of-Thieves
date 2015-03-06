using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_CameraGUI : MonoBehaviour {

	public GameObject[] thieves;
	public GameObject[] spawns;
	public GameObject thief;
	public GameObject blockOBJ;
	public Vector3 blockSpawn;


	public int playerTurn = 1; //0 = unit turn, 1 = p1, 2 = p2
	public int lastTurn = 0; //What was playerTurns last state
	public Text playerTurnUI;

	void Start(){

		//Spawn thieves at start of game
		spawns = GameObject.FindGameObjectsWithTag ("ThiefSpawn");
		foreach (GameObject spawn in spawns) {
			Instantiate (thief, spawn.transform.position, Quaternion.identity);
		}
	}

	void Update(){
		playerTurnUI.text = "Current turn: Player " + playerTurn.ToString();
	}

	void OnGUI(){






		//Buttons for testing
		if (GUILayout.Button ("Move One Tile")){
			DoMove();
		}
		if (GUILayout.Button ("Move Two Tiles")){
			DoMove2();
		}
	}

	void DoMove(){
		thieves = GameObject.FindGameObjectsWithTag ("Thief");
		
		foreach (GameObject thief in thieves) {
			thief.SendMessage ("DoMove", 1);
		}
	}
	void DoMove2(){
		thieves = GameObject.FindGameObjectsWithTag ("Thief");
		
		foreach (GameObject thief in thieves) {
			thief.SendMessage ("DoMove", 2);
		}
	}

	void EndTurn(){

		int tempLastTurn = playerTurn; //this is to get who went last before it changes, but also to not change the actual last change int before using it below

		//Get correct next turn (turns go 1,2,0,2,1,0,1,2,0,2,1 etc.etc.)
		if (playerTurn == 0 && lastTurn == 1) {
			playerTurn = 1;
		} else if (playerTurn == 0 && lastTurn == 2){
			playerTurn = 2;
		} else if (playerTurn == 1 && lastTurn == 2){
			playerTurn = 0;
		} else if (playerTurn == 2 && lastTurn == 1){
			playerTurn = 0;
		} else if (playerTurn == 2 && lastTurn == 0){
			playerTurn = 1;
		} else if ((playerTurn == 1) && (lastTurn == 0)){
			playerTurn = 2;
		}

		//change actual last turn
		lastTurn = tempLastTurn; 
	}
}
