using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance;

    //Holds all objects that need to move in the level. Monsters, grandma, cats, all of them.
	public Movement[] movingObjects;

    //Timer stuff
	public float timer;
	private float timerValue;

    //Signifies end location for level
    public Vector3 endSquare;
    //Holds all traps
    public Trap[] traps;

    //Signifies active, win, and lose states
	private bool executing;
    private bool won;
    private bool dead;

    //Creates instance of level manager, sets timer, disables all traps
	void Start () {
		instance = this;
		timerValue = timer;

        foreach (Trap t in traps)
        {
            t.GetComponent<SpriteRenderer>().enabled = false;
        }
	}
	
	void Update () {

        //Hitting space activates the level; No more movements can be input.
		if(Input.GetKeyDown(KeyCode.Space)) {
			executing = true;
		}

        //Hitting R resets the scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        //If the traps need to be activated, DO THAT JUNK
        foreach (Trap t in traps)
        {
            t.turnOnIfApplicable();
        }

        //if the level is active...
        if (executing) {
			if(timerValue < 0) {
				timerValue = timer;
				foreach (Movement m in movingObjects) {
                    if (m.active)
                    {
                        m.Move();
                    }
				}
			} else {
				timerValue -= Time.deltaTime;
			}
            HandleCollision();
		}

        //if win state is achieved...
        if (won)
        {
            executing = false;
            //Debug.Log("ya done won it");
        }

        //if u ded
        if (dead)
        {
            executing = false;
            //Debug.Log("U DED");
        }
	}

    //Handles collisions
	void HandleCollision() {
        //note: GRANDMA MUST ALWAYS BE AT INDEX ZERO IN MOVINGOBJECTS!
        //Check for gma collisions w monsters
        for (int i = 1; i < movingObjects.Length; i++)
        {
            if (movingObjects[0].transform.position == movingObjects[i].transform.position)
            {
                dead = true;
            }
        }

        //Check for monster collision w traps
        for (int i = 1; i < movingObjects.Length; i++)
        {
            for (int j = 0; j < traps.Length; j++)
            {
                if ((movingObjects[i].transform.position == traps[j].transform.position) && traps[j].active)
                {
                    movingObjects[i].active = false;
                }
            }
        }
        //Check if gma is at end square
        if (movingObjects[0].transform.position == endSquare)
        {
            won = true;
        }

    }

    //TODO: Refactor to account for multiple traps that all need to go at different times
    public void clickThing()
    {
        foreach (Trap t in traps)
        {
            t.active = true;
        }
    }
}
