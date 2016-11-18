using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{

    public Text sassText;

    public void Start()
    {
        sassText.text = " ";
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void BedroomPressed()
    {
        Destroy(PersistentMusic.instance.gameObject);
        SceneManager.LoadScene(3);
    }

    public void CafeteriaPressed()
    {
        Destroy(PersistentMusic.instance.gameObject);
        SceneManager.LoadScene(6);
    }

    public void PoolPressed()
    {
        Destroy(PersistentMusic.instance.gameObject);
        SceneManager.LoadScene(7);
    }

    public void GardenPressed()
    {
        Destroy(PersistentMusic.instance.gameObject);
        SceneManager.LoadScene(8);
    }

    public void EndPressed()
    {
        SceneManager.LoadScene(12);
    }

    public void NotActiveLevelPressed()
    {
        sassText.text = "Stick to the plan, whippersnapper";
    }

    public void oldLevelPressed()
    {
        sassText.text = "Never look back!";
    }
}
