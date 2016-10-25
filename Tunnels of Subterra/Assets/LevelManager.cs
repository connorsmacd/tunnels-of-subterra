using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public float levelSpeed = 1.5f;
    public float obstacleProbability = 0.05f;
    public int maxObstacles = 3;
    public float enemyProbability = 0.05f;
    public int maxEnemies = 2;

    public GameObject obs;

	void Start () {
        InvokeRepeating("chooseElements", 2.0f, 0.1f);
	}

    float getRand() {
        return Random.Range(0.0f, 1.0f);
    }

    void chooseElements() {
        if (getRand() <= obstacleProbability) {
            //generateObstacle();
            Instantiate(obs, new Vector3(0, -4.3f, 28.0f), new Quaternion());
        }
        if (getRand() <= enemyProbability) {
            //generateEnemy();
        }
    }
}

