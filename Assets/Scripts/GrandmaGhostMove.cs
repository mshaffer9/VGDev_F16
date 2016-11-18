using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrandmaGhostMove : MonoBehaviour {

	[HideInInspector]
	public Queue<KeyCode> path;

	[HideInInspector]
	public RectTransform rect;

	[HideInInspector]
	public Vector2 position;

    void Start () {
        path = new Queue<KeyCode>();
		rect = GetComponent<RectTransform>();
		position = rect.anchoredPosition;
    }

	public void ResetGhost(Vector2 position) {
		rect.anchoredPosition = position;
		this.position = position;
	}

	public bool UpdateGhostPosition (bool canMoveUp, bool canMoveDown, bool canMoveRight, bool canMoveLeft) {
		bool didMove = false;

		if (canMoveUp && Input.GetKeyDown(KeyCode.W) && position.y < 0f)
        {
            path.Enqueue(KeyCode.W);
			rect.anchoredPosition = position;
			position += Vector2.up*(LevelManager.instance.moveAmt);
			didMove = true;
        }
		if (canMoveLeft && Input.GetKeyDown(KeyCode.A) && position.x > 0f)
        {
            path.Enqueue(KeyCode.A);
			rect.anchoredPosition = position;
			position += Vector2.left* (LevelManager.instance.moveAmt);
			didMove = true;
        }
		if (canMoveDown && Input.GetKeyDown(KeyCode.S) && position.y > -50f*(LevelManager.instance.numSides-1))
        {
            path.Enqueue(KeyCode.S);
			rect.anchoredPosition = position;
			position += Vector2.down* (LevelManager.instance.moveAmt);
			didMove = true;
        }
		if (canMoveRight && Input.GetKeyDown(KeyCode.D) && position.x < 50f*(LevelManager.instance.numSides-1))
        {
            path.Enqueue(KeyCode.D);
			rect.anchoredPosition = position;
			position += Vector2.right* (LevelManager.instance.moveAmt);
			didMove = true;
        }

		return didMove;
    }

	void Update() {
		rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, position, Time.deltaTime*400f);
	}
}
