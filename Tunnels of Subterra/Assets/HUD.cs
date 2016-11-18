using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

    //private GameObject health, score;
    private Text health, score;
    private int pts = 0;
    private PlayerCharacter player;
    
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        health = GameObject.FindGameObjectWithTag("Health").GetComponent<Text>() as Text;
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>() as Text;
        health.text = "Health: " +player.getHealth().ToString();
        score.text = "Score: " +0.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        player.modifyScore(1);
        score.text = "Score: " +player.getScore().ToString();
        health.text = "Health: " + player.getHealth().ToString();
    }
}
