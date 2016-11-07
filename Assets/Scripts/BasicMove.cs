using UnityEngine;
using System.Collections;

public class BasicMove : Movement {

    //Sets if object is moving up/down or left/right
	public bool upDown;

    //speed - how many squares traveled each update
    //distance - how many moves are made before reversing directions
	public int speed, distance;

	private int distanceTraveled = 0;
	private bool movingPositive = true;

	public override void Move ()
	{
		base.Move ();
        int nSides = LevelManager.instance.numSides;

		if(upDown) {

			if(movingPositive) {
				if(distanceTraveled < distance) {
                    if (transform.position.y <= ((nSides - 1) + .5))
                    {
                        transform.Translate(new Vector3(0, 1f * speed, 0f));
                        distanceTraveled = distanceTraveled + speed;
                    } else
                    {
                        movingPositive = !movingPositive;
                        distanceTraveled = 0;
                    }
				} else if(distanceTraveled == distance) {
					movingPositive = false;
                    /*if (transform.position.y >= .5)
                    {
                        transform.Translate(new Vector3(0, -1f * speed, 0f));
                        distanceTraveled++;
                    } else
                    {
                        movingPositive = !movingPositive;
                        distanceTraveled = 0;
                    }*/

                    transform.Translate(new Vector3(0, -1f * speed, 0));
                    distanceTraveled = 0;
				}
			} else {
				if(distanceTraveled < distance) {
                    if (transform.position.y >= .5)
                    {
                        transform.Translate(new Vector3(0, -1f * speed, 0f));
                        distanceTraveled++;
                    }
                    else
                    {
                        movingPositive = !movingPositive;
                        distanceTraveled = 0;
                    }
                } else if(distanceTraveled == distance) {
                    /*movingPositive = true;	
                    if (transform.position.y <= ((nSides - 1) + .5))
                    {
                        transform.Translate(new Vector3(0, 1f * speed, 0f));
                        distanceTraveled++;
                    }
                    else
                    {
                        movingPositive = !movingPositive;
                        distanceTraveled = 0;
                    }*/
                    transform.Translate(new Vector3(0, 1f * speed, 0f));
                    distanceTraveled = 0;
                }
			}
		} else {
			if(movingPositive) {
				if(distanceTraveled < distance) {
                    if (transform.position.x <= ((nSides -1) + .5))
                    {
                        transform.Translate(new Vector3(1f * speed, 0f, 0f));
                        distanceTraveled++;
                    } else
                    {
                        movingPositive = !movingPositive;
                        distanceTraveled = 0;
                    }
				} else if(distanceTraveled == distance) {
					movingPositive = false;	
                    if (transform.position.x >= .5)
                    {
                        transform.Translate(new Vector3(-1f * speed, 0f, 0f));
                        distanceTraveled++;
                    } else
                    {
                        movingPositive = !movingPositive;
                        distanceTraveled = 0;
                    }
				}
			} else {
				if(distanceTraveled > 0) {
                    if (transform.position.x >= .5)
                    {
                        transform.Translate(new Vector3(-1f * speed, 0f, 0f));
                        distanceTraveled++;
                    }
                    else
                    {
                        movingPositive = !movingPositive;
                        distanceTraveled = 0;
                    }
                } else if(distanceTraveled == 0) {
					movingPositive = true;	
                    if (transform.position.x <= ((nSides - 1) + .5))
                    {
                        transform.Translate(new Vector3(1f * speed, 0f, 0f));
                        distanceTraveled++;
                    }
                    else
                    {
                        movingPositive = !movingPositive;
                        distanceTraveled = 0;
                    }
                }
			}
		}
	}
}
