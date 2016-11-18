using UnityEngine;
using System.Collections;

public class SingleDirectionMovement : Movement {

    //Sets if object is moving up/down or left/right
	public bool upDown;

    //distance - how many moves are made before reversing directions
	public int distance;

	private int distanceTraveled = 0;
	private bool movingPositive = true;
	private int numSides;

	protected override void Start ()
	{
		base.Start ();
		numSides = LevelManager.instance.numSides;
		if(distance < 0) {
			movingPositive = false;
		} else {
			movingPositive = true;
		}
		this.position = rect.anchoredPosition;
	}

	public override void Reset (Vector2 resetPosition)
	{
		base.Reset (resetPosition);
		distanceTraveled = 0;
		if(distance < 0) {
			movingPositive = false;
		} else {
			movingPositive = true;
		}
		rect.anchoredPosition = resetPosition;
		this.position = resetPosition;
	}


	public override void Move (bool canMoveUp, bool canMoveDown, bool canMoveRight, bool canMoveLeft)
	{
		base.Move (canMoveUp, canMoveDown, canMoveRight, canMoveLeft);
        if (trap != null && trap.active)
        {
            if (trap.changeDir)
            {
                upDown = !upDown;
                trap.active = false;
            } //don't need to deactivate the trap if it's just sticky

            return;
        }

		if(upDown) {
			if(movingPositive) {
				if(distanceTraveled < Mathf.Abs(distance)) {
					if (canMoveUp && rect.anchoredPosition.y < -LevelManager.instance.moveAmt) {
						position += Vector2.up*LevelManager.instance.moveAmt;
						StartCoroutine(MoveAnim(position));
                        distanceTraveled++;
					} else {
                        movingPositive = !movingPositive;
                        distanceTraveled = 0;
                    }
				} else if(distanceTraveled == Mathf.Abs(distance)) {
					movingPositive = false;
                    distanceTraveled = 0;
				}
			} else {
				if(distanceTraveled < Mathf.Abs(distance)) {
					if (canMoveDown && rect.anchoredPosition.y > -LevelManager.instance.moveAmt*numSides) {
						position -= Vector2.up*LevelManager.instance.moveAmt;
						StartCoroutine(MoveAnim(position));
						distanceTraveled++;
					} else {
						movingPositive = !movingPositive;
						distanceTraveled = 0;
					}
				} else if(distanceTraveled == Mathf.Abs(distance)) {
					movingPositive = true;
					distanceTraveled = 0;
				}
			}
		} else {
			if(movingPositive) {
				if(distanceTraveled < Mathf.Abs(distance)) {
					if (canMoveRight && rect.anchoredPosition.x < LevelManager.instance.moveAmt*(numSides-1)) {
						position += Vector2.right* LevelManager.instance.moveAmt;
						StartCoroutine(MoveAnim(position));
						distanceTraveled++;
					} else {
						movingPositive = !movingPositive;
						distanceTraveled = 0;
					}
				} else if(distanceTraveled == Mathf.Abs(distance)) {
					movingPositive = false;
					distanceTraveled = 0;
				}
			} else {
				if(distanceTraveled < Mathf.Abs(distance)) {
					if (canMoveLeft && rect.anchoredPosition.x > 0) {
                        position -= Vector2.right * LevelManager.instance.moveAmt;

                        StartCoroutine(MoveAnim(position));
						distanceTraveled++;
					} else {
						movingPositive = !movingPositive;
						distanceTraveled = 0;
					}
				} else if(distanceTraveled == Mathf.Abs(distance)) {
					movingPositive = true;
					distanceTraveled = 0;
				}
			}
		}
	}
}
