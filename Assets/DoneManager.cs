using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoneManager : MonoBehaviour {

	void Update () {
	    if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(0);
        }
	}
}
