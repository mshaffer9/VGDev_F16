using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{

    public static PersistentMusic instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}
