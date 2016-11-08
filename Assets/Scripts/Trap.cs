using UnityEngine;
using System.Collections;

//Traps!
//TODO: refactor to handle types, 
public class Trap : MonoBehaviour {

	public bool active, interruptMovement;

	protected RectTransform rect;

	protected void Start() {
		rect = GetComponent<RectTransform>();
	}

    /// <summary>
    /// Override this function to handle HOW the trap is 'sprung'
    /// </summary>
    /// <param name="target">Target.</param>
	public virtual void Spring(Movement target) {	}

	/// <summary>
	/// Override this to make the target move a specific way
	/// </summary>
	public virtual void HandleTrap(Movement target) { }
}
