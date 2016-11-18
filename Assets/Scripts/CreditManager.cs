using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour {

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(0);
        }
    }
}
