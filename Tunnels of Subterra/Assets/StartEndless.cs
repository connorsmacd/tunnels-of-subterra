using UnityEngine;
using System.Collections;

public class StartEndless : MonoBehaviour {

    //starts the game
    public void onClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Test Level");
    }
}
