using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public float levelSpeed = 1.5f;
    public float obstacleProbability = 0.05f;
    public int maxObstacles = 3;
    public float enemyProbability = 0.05f;
    public int maxEnemies = 2;
    public Material levelMaterial;

	private ObstacleManager obstacleManager;

	void Start () {
		obstacleManager = gameObject.GetComponent<ObstacleManager>();
        InvokeRepeating("chooseElements", 2.0f, 0.1f);
	}

    float getRand() {
        return Random.Range(0.0f, 1.0f);
    }

    void chooseElements() {
		int obstacleCount = GameObject.FindGameObjectsWithTag("Obstacle").Length;
		if ((getRand() <= obstacleProbability) && (obstacleCount <= maxObstacles)) {
			obstacleManager.generateObstacle();
        }
        if (getRand() <= enemyProbability) {
            //generateEnemy();
        }
    }
}

