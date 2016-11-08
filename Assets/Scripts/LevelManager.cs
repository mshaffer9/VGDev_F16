using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance;

    /// <summary>
	/// How many sides does the level have?
    /// </summary>
    public int numSides = 5;

	/// <summary>
	/// The max number of moves the player can make
	/// </summary>
	public int maxMoves = 10;
	private int movesLeft;
	public Text moveCounter;

	/// <summary>
	/// Automatically finds all entities that move
	/// </summary>
	private Movement[] entities;
	private Vector2[] startingPositions;
	private GrandmaGhostMove ghost;
	private GrandmaMove granny;

    //Timer stuff
	public float timer;
	private float timerValue;
	private bool grannyMovedLast;

    //Signifies end location for level
	public RectTransform spawnPoint, winPoint;

	//Contains all the obstacles in the scene that no one can walk on
	public RectTransform[] obstacles;

    //Which traps are you allowed on this level?
	public GameObject[] availableTraps;

	//stores trap ghosts
	public GameObject[] trapGhosts;

	//How many of each trap are you allowed on this level?
	public int[] trapCount;

	//Maps each available trap to the number available for that trap
	private Dictionary<Trap, int> traps;

	//Holds all the traps that have been spawned
	private List<GameObject> spawnedTraps, spawnedTrapGhosts;

	//reference to the inventory parent
	public RectTransform inventory;

	//Holds the cat
	public RectTransform kitty;
	private bool kittyCollected;

	//State machine manager
	public enum GameState {Planning, Executing, Win, Dead};
	private GameState state;

	//Reference to the world parent
	public RectTransform world;

	//Reference to the game state text object
	public Text gameStateText;

	#region MonoBehavior Functions
    //Creates instance of level manager, sets timer
	void Start () {
		instance = this;
		entities = FindObjectsOfType<Movement>();
		ghost = FindObjectOfType<GrandmaGhostMove>();
		granny = FindObjectOfType<GrandmaMove>();

		traps = new Dictionary<Trap, int>();
		for(int i = 0; i < trapCount.Length; i++) {
			traps.Add(availableTraps[i].GetComponent<Trap>(), trapCount[i]);
			inventory.GetChild(i).GetChild(0).GetComponent<Text>().text = traps[availableTraps[i].GetComponent<Trap>()].ToString();
		}

		spawnedTraps = new List<GameObject>();
		spawnedTrapGhosts = new List<GameObject>();

		startingPositions = new Vector2[entities.Length];
		for(int i = 0; i < entities.Length; i++) {
			startingPositions[i] = entities[i].GetComponent<RectTransform>().anchoredPosition;
		}

		movesLeft = maxMoves;
		moveCounter.text = movesLeft.ToString();
    }
	
	void Update () {

		switch(state) {

		case GameState.Planning:
			HandlePlanning();
			break;
		case GameState.Executing:
			HandleExecuting();
			break;
		case GameState.Win:
			HandleWin();
			break;
		case GameState.Dead:
			HandleDead();
			break;

		}

        //Hitting R resets the scene. TODO: FIX THIS WHEN MENUS ARE SET
        if (Input.GetKeyDown(KeyCode.R))
        {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

	}
	#endregion



	#region Game State Machine
	private void HandlePlanning() {
		bool canMoveUp = true, canMoveDown = true, canMoveLeft = true, canMoveRight = true;
		for(int i = 0; i < obstacles.Length; i++) {
			if(ghost.position + Vector2.up*50f == obstacles[i].anchoredPosition) canMoveUp = false;
			if(ghost.position + Vector2.down*50f == obstacles[i].anchoredPosition) canMoveDown = false;
			if(ghost.position + Vector2.left*50f == obstacles[i].anchoredPosition) canMoveLeft = false;
			if(ghost.position + Vector2.right*50f == obstacles[i].anchoredPosition) canMoveRight = false;
		}

		//Updates the ghost's position only in the planning stage
		if(movesLeft > 0 && ghost.UpdateGhostPosition(canMoveUp, canMoveDown, canMoveRight, canMoveLeft)) {
			movesLeft--;
			moveCounter.text = movesLeft.ToString();
		}

		//Places traps
		for(int i = 0; i < availableTraps.Length; i++) {
			if(Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), "Alpha" + (i+1))) && traps[availableTraps[i].GetComponent<Trap>()] > 0) {
				ghost.path.Enqueue((KeyCode)Enum.Parse(typeof(KeyCode), "Alpha" + (i+1)));
				traps[availableTraps[i].GetComponent<Trap>()]--;
				inventory.GetChild(i).GetChild(0).GetComponent<Text>().text = traps[availableTraps[i].GetComponent<Trap>()].ToString();
				GameObject temp = (GameObject)GameObject.Instantiate(trapGhosts[i], world);
				temp.GetComponent<RectTransform>().anchoredPosition = ghost.position;
				temp.transform.localScale = Vector3.one;
				temp.transform.SetSiblingIndex(ghost.transform.GetSiblingIndex() - 1);
				spawnedTrapGhosts.Add(temp);
			}
		}

		//Changes the state to the executing stage
		if(Input.GetKeyDown(KeyCode.Space)) {
			GoGrannyGo();
		}

	}

	private void HandleExecuting() {
		if(timerValue < 0) {
			timerValue = timer;
			if(grannyMovedLast) {
				foreach (Movement m in entities) {
					if (m.active) {
						bool canMoveUp = true, canMoveDown = true, canMoveLeft = true, canMoveRight = true;
						for(int i = 0; i < obstacles.Length; i++) {
							if(m.position + Vector2.up*50f == obstacles[i].anchoredPosition) canMoveUp = false;
							if(m.position + Vector2.down*50f == obstacles[i].anchoredPosition) canMoveDown = false;
							if(m.position + Vector2.left*50f == obstacles[i].anchoredPosition) canMoveLeft = false;
							if(m.position + Vector2.right*50f == obstacles[i].anchoredPosition) canMoveRight = false;
						}

						m.Move(canMoveUp, canMoveDown, canMoveRight, canMoveLeft);
					}
				}
			} else {
				int spawn = granny.Move();
				if(spawn != -1) {
					GameObject temp = (GameObject)GameObject.Instantiate(availableTraps[spawn-1], world);
					temp.GetComponent<RectTransform>().anchoredPosition = granny.position;
					temp.transform.localScale = Vector3.one;
					temp.transform.SetSiblingIndex(granny.transform.GetSiblingIndex() - 1);
					spawnedTraps.Add(temp);
				}
			}
			grannyMovedLast = !grannyMovedLast;
		} else {
			timerValue -= Time.deltaTime;
		}
		HandleCollision();
	}

	private void HandleDead() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			ResetGranny();
		}
	}

	private void HandleWin() {

	}
	#endregion


	#region Helper Functions
	void HandleCollision() {
		foreach (Movement m in entities) {
			if(m.active) {
				if(m.position == granny.position) {
					KillGranny();
				}
				for(int i = 0; i < spawnedTraps.Count; i++) {
					spawnedTraps[i].GetComponent<Trap>().Spring(m);
				}
			}
		}
		if(granny.position == winPoint.anchoredPosition && kittyCollected) {
			WinGranny();
		} else if(granny.position == kitty.anchoredPosition) {
			kittyCollected = true;
			kitty.GetComponent<CanvasGroup>().alpha = 0f;
		} else if(granny.path.Count == 0) {
			ResetGranny();
		}
    }

	void KillGranny() {
		state = GameState.Dead;
		gameStateText.text = state.ToString();
	}

	void ResetGranny() {
		state = GameState.Planning;
		granny.ResetGranny(spawnPoint.anchoredPosition);
		ghost.ResetGhost(spawnPoint.anchoredPosition);
		gameStateText.text = state.ToString();

		for(int i = 0; i < entities.Length; i++) {
			entities[i].Reset(startingPositions[i]);
		}

		movesLeft = maxMoves;
		moveCounter.text = movesLeft.ToString();
		kitty.GetComponent<CanvasGroup>().alpha = 1f;

		for(int i = 0; i < spawnedTraps.Count; i++) {
			Destroy(spawnedTraps[i]);
		}
		spawnedTraps.Clear();

		for(int i = 0; i < spawnedTrapGhosts.Count; i++) {
			Destroy(spawnedTrapGhosts[i]);
		}
		spawnedTrapGhosts.Clear();

		for(int i = 0; i < trapCount.Length; i++) {
			traps[availableTraps[i].GetComponent<Trap>()] = trapCount[i];
			//Sets the available number of traps
			inventory.GetChild(i).GetChild(0).GetComponent<Text>().text = traps[availableTraps[i].GetComponent<Trap>()].ToString();
		}
	}

	void GoGrannyGo() {
		state = GameState.Executing;
		granny.path = ghost.path;
		gameStateText.text = state.ToString();
	}

	void WinGranny() {
		state = GameState.Win;
		gameStateText.text = state.ToString();
	}

	public float MoveLerpValue() {
		return timerValue/timer;
	}

	#endregion
}
