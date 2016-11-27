using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

    //private GameObject health, score;
    private Text health, score, prevScore;
    //private int pts = 0;
    private PlayerCharacter player;
    
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        health = GameObject.FindGameObjectWithTag("Health").GetComponent<Text>() as Text;
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>() as Text;
        prevScore = GameObject.FindGameObjectWithTag("PrevScore").GetComponent<Text>() as Text;
        prevScore.text = "Last Run's Score: " +PlayerPrefs.GetFloat("previousScore");
        health.text = "Health: " +player.getHealth().ToString();
        score.text = "Score: " +0.ToString();
        health.color = new Color(0f, 255f / 255f, 65f / 255f);



        

    }
	
	// Update is called once per frame
	void Update () {
        player.modifyScore(1);
        score.text = "Score: " +player.getScore().ToString();
        health.text = "Health: " + player.getHealth().ToString();

        //changes color of health value so that the player notices they are close to dying
        if(player.getHealth() <= 20)
        {
            health.color = Color.red;
        }
        if(player.getHealth() <= 0)
        {
            //player iz ded
            player.heal(100f);
            playerDied(player.getScore());
        }
        
    }
    void playerDied(float finalScore)
    {
        Text gameOver = GameObject.FindGameObjectWithTag("GameOver").GetComponent<Text>();
        gameOver.text = "GAME OVER\n Final Score: " + finalScore;
        Destroy(GameObject.FindGameObjectWithTag("Health"));
        Destroy(GameObject.FindGameObjectWithTag("Score"));
        StartCoroutine(endGame());
        PlayerPrefs.SetFloat("previousScore", finalScore);

        float[] scores = PlayerPrefsX.GetFloatArray("Scores");
        for(int j = 0; j < scores.Length; j++)
        {
            if (finalScore > scores[j])
            {
                scores[j] = finalScore;
                break;
            }
        }
        PlayerPrefsX.SetFloatArray("Scores", scores);
    }

    //waits for 5 seconds then returns the user to the main menu
    IEnumerator endGame()
    {
        yield return new WaitForSecondsRealtime(5);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
