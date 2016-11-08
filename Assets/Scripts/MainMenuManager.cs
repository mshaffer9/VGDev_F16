using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public void PlayPressed() {
		SceneManager.LoadScene(2);
	}

	public void CreditsPressed() {
		SceneManager.LoadScene("Credits");
	}
}
