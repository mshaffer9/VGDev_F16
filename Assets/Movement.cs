using UnityEngine;
using System.Collections;

//Parent for different types of object movement
public class Movement : MonoBehaviour {

	[HideInInspector]
    public bool active = true;

	[HideInInspector]
	public Trap trap;

	[HideInInspector]
	public RectTransform rect;

	[HideInInspector]
	public Vector2 position;

	protected virtual void Start() {
		rect = GetComponent<RectTransform>();
	}

	public virtual void Move(bool canMoveUp, bool canMoveDown, bool canMoveRight, bool canMoveLeft) {
		if(trap != null) trap.HandleTrap(this);
	}

	public virtual void Reset(Vector2 resetPosition) { }

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
