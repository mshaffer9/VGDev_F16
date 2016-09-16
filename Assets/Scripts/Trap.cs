using UnityEngine;
using System.Collections;

//Traps!
//TODO: refactor to handle types, 
public class Trap : MonoBehaviour {

    public bool active;
    //Assign type, handle action when called on accordingly
    public enum TYPE { Sticky, Pushy, Switchy};
	
    //Makes trap visible if active
	public void turnOnIfApplicable() {
	    if (active)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
	}

    //springs active traps
    public void spring()
    {
        //TODO: handle each type of trap
    }
}
