using UnityEngine;
using System.Collections;

public class GrandmaGhostMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W)) {
			transform.Translate(new Vector3(0,2f,0f));
		}
		if(Input.GetKeyDown(KeyCode.A)) {
			transform.Translate(new Vector3(-2f,0f,0f));
		}
		if(Input.GetKeyDown(KeyCode.S)) {
			transform.Translate(new Vector3(0,-2f,0f));
		}
		if(Input.GetKeyDown(KeyCode.D)) {
			transform.Translate(new Vector3(2f,0f,0f));
		}
	}
}
