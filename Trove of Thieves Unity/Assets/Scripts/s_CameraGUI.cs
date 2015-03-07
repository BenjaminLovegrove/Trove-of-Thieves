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
	public Text rollText;
	public Text p1Stash;
	public Text p2Stash;
	public Image p1Buy;
	public Image p2Buy;
	public Button endTurnButton;

	public int moveRoll;
	public float moveTurnTimer = 0;
	public bool moving = false;

	public int lastRoll;
	public int player1Stash = 0;
	public int player2Stash = 0;

	public AudioClip rollDice;

	void Start(){

		//p1 first turn roll
		lastRoll = Random.Range (2,12);
		audio.PlayOneShot(rollDice);
		player1Stash += lastRoll;

		//Spawn thieves at start of game
		spawns = GameObject.FindGameObjectsWithTag ("ThiefSpawn");
		foreach (GameObject spawn in spawns) {
			Instantiate (thief, spawn.transform.position, Quaternion.identity);
		}
	}

	void Update(){
		//End unit moving turn
		moveTurnTimer -= Time.deltaTime;
		if (moving == true && moveTurnTimer <= 0) {
			moving = false;
			EndTurn();
		}

		//Change UI texts
		if (playerTurn == 1) {
			p1Buy.enabled = true;
		} else {
			p1Buy.enabled = false;
		}

		if (playerTurn == 2) {
			p2Buy.enabled = true;
		} else {
			p2Buy.enabled = false;
		}

		if (playerTurn == 0) {
			playerTurnUI.text = "Thief move!";
		} else {
			playerTurnUI.text = "Player " + playerTurn.ToString ();
		}

		rollText.text = lastRoll.ToString ();
		p1Stash.text = player1Stash.ToString ();
		p2Stash.text = player2Stash.ToString ();


		//Enable/disable "end turn button" when player/units turn
		if (playerTurn == 0) {
			endTurnButton.enabled = false;
			endTurnButton.image.enabled = false;
		} else if (playerTurn != 0) {
			endTurnButton.enabled = true;
			endTurnButton.image.enabled = true;

		}
	}

	void EndTurn(){ // This is called when end turn button is pressed

		int tempLastTurn = playerTurn; //this is to get who went last before it changes, but also to not change the actual last change int before using it below

		//Get correct next turn (turns go 1,2,0,2,1,0,1,2,0,2,1 etc.etc.)
		if (playerTurn == 0 && lastTurn == 1) {
			lastRoll = Random.Range (2,12);
			audio.PlayOneShot(rollDice);
			player1Stash += lastRoll;
			playerTurn = 1;
		} else if (playerTurn == 0 && lastTurn == 2){
			lastRoll = Random.Range (2,12);
			audio.PlayOneShot(rollDice);
			player2Stash += lastRoll;
			playerTurn = 2;
		} else if (playerTurn == 1 && lastTurn == 2){
			playerTurn = 0;
			UnitTurn();
		} else if (playerTurn == 2 && lastTurn == 1){
			playerTurn = 0;
			UnitTurn();
		} else if (playerTurn == 2 && lastTurn == 0){
			lastRoll = Random.Range (2,12);
			audio.PlayOneShot(rollDice);
			player1Stash += lastRoll;
			playerTurn = 1;
		} else if ((playerTurn == 1) && (lastTurn == 0)){
			lastRoll = Random.Range (2,12);
			audio.PlayOneShot(rollDice);
			player2Stash += lastRoll;
			playerTurn = 2;
		}

		//change actual last turn
		lastTurn = tempLastTurn; 
	}

	void UnitTurn(){
		//Get Thieves
		thieves = GameObject.FindGameObjectsWithTag ("Thief");

		moving = true;
		lastTurn = 0;
		lastRoll = Random.Range (2, 12);
		audio.PlayOneShot(rollDice);
		moveRoll = lastRoll;

		moveTurnTimer = 4;
		foreach (GameObject thief in thieves) {
			thief.SendMessage ("DoMove", moveRoll);
		}
	}
}
