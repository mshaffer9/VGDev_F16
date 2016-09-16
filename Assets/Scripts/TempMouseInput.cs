using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

//TODO: replace this with functional UI prototype
public class TempMouseInput : MonoBehaviour {

    void OnMouseDown()
    {
        LevelManager.instance.clickThing();
    }

        void Start () {
	
	}
	
	void Update () {
	
	}
}
