using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class InstructionManager : MonoBehaviour
{

    public void PlayPressed()
    {
        SceneManager.LoadScene(1); //Level select
    }
}
