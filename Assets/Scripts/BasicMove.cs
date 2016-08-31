using UnityEngine;
using System.Collections;

public class BasicMove : Movement {

	public bool upDown;

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
					transform.Translate(new Vector3(0,2f,0f));
				} else if(distanceTraveled == distance) {
					movingPositive = false;	
					distanceTraveled--;
					transform.Translate(new Vector3(0,-2f,0f));
				}
			} else {
				if(distanceTraveled > 0) {
					distanceTraveled--;
					transform.Translate(new Vector3(0,-2f,0f));
				} else if(distanceTraveled == 0) {
					movingPositive = true;	
					distanceTraveled++;
					transform.Translate(new Vector3(0,2f,0f));
				}
			}
		} else {
			if(movingPositive) {
				if(distanceTraveled < distance) {
					distanceTraveled++;
					transform.Translate(new Vector3(2,0f,0f));
				} else if(distanceTraveled == distance) {
					movingPositive = false;	
					distanceTraveled--;
					transform.Translate(new Vector3(-2f,0f,0f));
				}
			} else {
				if(distanceTraveled > 0) {
					distanceTraveled--;
					transform.Translate(new Vector3(-2f,0f,0f));
				} else if(distanceTraveled == 0) {
					movingPositive = true;	
					distanceTraveled++;
					transform.Translate(new Vector3(2f,0f,0f));
				}
			}
		}
	}
}
