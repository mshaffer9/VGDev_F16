using UnityEngine;
using System.Collections;

//Parent for different types of object movement
public class Movement : MonoBehaviour {

    public bool active = true;

	public virtual void Move() {	}

}
