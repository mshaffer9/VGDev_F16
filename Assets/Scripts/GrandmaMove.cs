using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrandmaMove : MonoBehaviour {

	[HideInInspector]
	public RectTransform rect;

	[HideInInspector]
	public Queue<KeyCode> path;

	[HideInInspector]
	public Vector2 position;

	void Start() {
		rect = GetComponent<RectTransform>();
        this.position = rect.anchoredPosition;
	}

	public void ResetGranny(Vector2 position) {
		StopAllCoroutines();
		rect.anchoredPosition = position;
		this.position = position;
		path.Clear();
	}

	public int Move() {
		if (path.Count > 0) {
			KeyCode nextMovement = path.Dequeue();
			switch(nextMovement) {

			case KeyCode.W:
				position += Vector2.up* (LevelManager.instance.moveAmt);
				StartCoroutine(MoveAnim(position));
				break;
			case KeyCode.A:
				position += Vector2.left* (LevelManager.instance.moveAmt);
				StartCoroutine(MoveAnim(position));
				break;
			case KeyCode.S:
				position += Vector2.down* (LevelManager.instance.moveAmt);
				StartCoroutine(MoveAnim(position));
				break;
			case KeyCode.D:
				position += Vector2.right* (LevelManager.instance.moveAmt);
				StartCoroutine(MoveAnim(position));
				break;
			default:
				return int.Parse(nextMovement.ToString().Substring(nextMovement.ToString().Length - 1));
				break;

			}
		}
		return -1;
	}

	protected IEnumerator MoveAnim(Vector2 newPosition) {
		float timer = 0f;

		while(timer < 1f) {
			timer += Time.deltaTime*10f;
			rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, newPosition, timer);
			yield return new WaitForEndOfFrame();
		}
		rect.anchoredPosition = newPosition;
		yield return null;
	}
}
