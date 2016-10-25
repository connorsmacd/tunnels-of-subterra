using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public float levelSpeed = 1.5f;
    public float obstacleProbability = 0.05f;
    public int maxObstacles = 3;
    public float enemyProbability = 0.05f;
    public int maxEnemies = 2;

	void Start () {
        InvokeRepeating("chooseElements", 2.0f, 0.1f);
	}

    float getRand() {
        return Random.Range(0.0f, 1.0f);
    }

    void chooseElements() {
        while(true) {
            if (getRand() <= obstacleProbability) {
                //generateObstacle();
            }
            if (getRand() <= enemyProbability) {
                //generateEnemy();
            }
        }
    }
}

