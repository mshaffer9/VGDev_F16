using UnityEngine;
using System.Collections;

public class BasicMove : Movement {

    //Sets if object is moving up/down or left/right
	public bool upDown;

    //speed - how many squares traveled each update
    //distance - how many moves are made before reversing directions
	public int speed, distance;

	private int distanceTraveled;
	private bool movingPositive = true;

	public override void Move ()
	{
		base.Move ();

		if(upDown) {
			if(movingPositive) {
				if(distanceTraveled < distance) {
					distanceTraveled++;
					transform.Translate(new Vector3(0,1f*speed,0f));
				} else if(distanceTraveled == distance) {
					movingPositive = false;	
					distanceTraveled--;
					transform.Translate(new Vector3(0,-1f*speed,0f));
				}
			} else {
				if(distanceTraveled > 0) {
					distanceTraveled--;
					transform.Translate(new Vector3(0,-1f*speed,0f));
				} else if(distanceTraveled == 0) {
					movingPositive = true;	
					distanceTraveled++;
					transform.Translate(new Vector3(0,1f*speed,0f));
				}
			}
		} else {
			if(movingPositive) {
				if(distanceTraveled < distance) {
					distanceTraveled++;
					transform.Translate(new Vector3(1f*speed,0f,0f));
				} else if(distanceTraveled == distance) {
					movingPositive = false;	
					distanceTraveled--;
					transform.Translate(new Vector3(-1f*speed,0f,0f));
				}
			} else {
				if(distanceTraveled > 0) {
					distanceTraveled--;
					transform.Translate(new Vector3(-1f*speed,0f,0f));
				} else if(distanceTraveled == 0) {
					movingPositive = true;	
					distanceTraveled++;
					transform.Translate(new Vector3(1f*speed,0f,0f));
				}
			}
		}
	}
}
