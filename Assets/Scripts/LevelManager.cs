using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance;

    //How many sides does the level have?
    public int numSides = 5;

	public Movement[] monsters;
    public Movement granny;

    //Timer stuff
	public float timer;
	private float timerValue;

    //Signifies end location for level
    public Movement endSquare;
    //Holds all traps
    public Trap[] traps;

    //Keeping track of UI levels
    public GameObject actionLayer;
    public GameObject trapLayer;
    public GameObject ghostLayer;

    //Signifies active, win, and lose states
	private bool executing;
    private bool won;
    private bool dead;

    //Creates instance of level manager, sets timer
	void Start () {
		instance = this;
		timerValue = timer;
    }
	
	void Update () {

        //Hitting space activates the level; No more movements can be input.
		if(Input.GetKeyDown(KeyCode.Space)) {
            populateTrapArray();
			executing = true;
		}

        //Hitting R resets the scene. TODO: FIX THIS WHEN MENUS ARE SET
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        //if u ded
        if (dead)
        {
            executing = false;
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
	}

    //TODO: MAKE THIS NOT SUCK ASS
	void HandleCollision() {
        //Check for gma collisions w monsters
        for (int i = 0; i < monsters.Length; i++)
        {
            if (granny.transform.parent.Equals(monsters[i].transform.parent))
            {
                dead = true;
                granny.GetComponent<Image>().enabled = false;
            }
        }

        //Check for monster collision w traps. ACCOUNT FOR LAYERS
        for (int i = 0; i < monsters.Length; i++)
        {
            for (int j = 0; j < traps.Length; j++)
            {
                if (monsters[i].transform.GetSiblingIndex() == traps[j].transform.GetSiblingIndex())
                {
                    monsters[i].active = false;
                }
            }
        }

        //Check if gma is at end square
        /*if (granny.transform.GetSiblingIndex() == endSquare.transform.GetSiblingIndex())
        {
            won = true;
        }*/

    }

    public void populateTrapArray()
    {
        //aaaaaa
    }
}
