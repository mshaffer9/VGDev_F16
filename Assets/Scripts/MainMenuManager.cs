using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public GameObject music;

	public void PlayPressed() {
        DontDestroyOnLoad(music);
		SceneManager.LoadScene(2);
	}

	public void CreditsPressed() {
        DontDestroyOnLoad(music);
        SceneManager.LoadScene(4);
	}
}
