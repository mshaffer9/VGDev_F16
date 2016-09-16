using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance;

	public Movement[] movingObjects;

	public float timer;
	private float timerValue;

	private bool executing;

	void Start () {
		instance = this;
		timerValue = timer;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space)) {
			executing = true;
		}

		if(executing) {
			if(timerValue < 0) {
				timerValue = timer;
				foreach (Movement m in movingObjects) {
					m.Move();
				}
			} else {
				timerValue -= Time.deltaTime;
			}
		}
	}

	void HandleCollision() {
		//TODO: do this shit
	}
}
