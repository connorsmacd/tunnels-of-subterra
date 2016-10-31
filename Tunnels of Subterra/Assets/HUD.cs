using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {

    //private GameObject health, score;
    private Text health, score;
    private int pts = 0;
    
	// Use this for initialization
	void Start () {
        health = GameObject.FindGameObjectWithTag("Health").GetComponent<Text>() as Text;
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>() as Text;
        health.text = "Health: " +100.ToString();
        score.text = "Score: " +0.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        pts += 1;
        score.text = "Score: " +pts.ToString();

    }
}
