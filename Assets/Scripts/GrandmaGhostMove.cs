using UnityEngine;
using System.Collections;

public class GrandmaGhostMove : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
        //Moves with key presses
		if(Input.GetKeyDown(KeyCode.W)) {
			transform.Translate(new Vector3(0,1f,0f));
		}
		if(Input.GetKeyDown(KeyCode.A)) {
			transform.Translate(new Vector3(-1f,0f,0f));
		}
		if(Input.GetKeyDown(KeyCode.S)) {
			transform.Translate(new Vector3(0,-1f,0f));
		}
		if(Input.GetKeyDown(KeyCode.D)) {
			transform.Translate(new Vector3(1f,0f,0f));
		}
	}
}
