using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelDesign : MonoBehaviour {

	//public static LevelManager instance;

    //Holds all objects that need to move in the level. Monsters, grandma, cats, all of them.
	public Movement[] monsters;
    public Movement granny;

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
				foreach (Movement m in monsters) {
                    if (m.active)
                    {
                        m.Move();
                    }
				}
                granny.Move();
			} else {
				timerValue -= Time.deltaTime;
			}
            HandleCollision();
		}

        //if win state is achieved...
        if (won)
        {
            executing = false;
        }

        //if u ded
        if (dead)
        {
            executing = false;
        }
	}

    //Handles collisions
	void HandleCollision() {
        //Check for gma collisions w monsters
        for (int i = 0; i < monsters.Length; i++)
        {
            if (granny.transform.position == monsters[i].transform.position)
            {
                dead = true;
            }
        }

        //Check for monster collision w traps
        for (int i = 0; i < monsters.Length; i++)
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
        if (granny.transform.position == endSquare)
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
