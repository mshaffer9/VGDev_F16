using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrandmaMovement : Movement {

    //Keeps track of movement path, to be executed when the level activates
	private Queue<KeyCode> path;

	void Start () {
		path = new Queue<KeyCode>();
	}
	
    //Queue's grandma's movements with key presses
	void Update () {
		if(Input.GetKeyDown(KeyCode.W)) {
			path.Enqueue(KeyCode.W);
		}
		if(Input.GetKeyDown(KeyCode.A)) {
			path.Enqueue(KeyCode.A);
        }
		if(Input.GetKeyDown(KeyCode.S)) {
			path.Enqueue(KeyCode.S);
        }
		if(Input.GetKeyDown(KeyCode.D)) {
			path.Enqueue(KeyCode.D);
        }
	}

    //Takes grandma through the movement queue
	public override void Move ()
	{
		if(path.Count > 0) {
			KeyCode key = path.Dequeue();
			if(key == KeyCode.W) {
				transform.Translate(new Vector3(0,1f,0f));
			}
			else if(key == KeyCode.A) {
				transform.Translate(new Vector3(-1f,0f,0f));
			}
			else if(key == KeyCode.S) {
				transform.Translate(new Vector3(0,-1f,0f));
			}
			else if(key == KeyCode.D) {
				transform.Translate(new Vector3(1f,0f,0f));
			}
		}
	}
}
