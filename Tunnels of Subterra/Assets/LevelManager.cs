using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Script: LevelManager.cs
 * Author: Connor S. MacDonald (B00632423)
 * 
 * This function determines the behaviour of a level and holds all
 * of the information that is required for the level.
 */
public class LevelManager : MonoBehaviour {

	// Speed that the ship will travel at
    public float levelSpeed = 100.0f;
	// Probability of an obstacle being generated per 100 milliseconds
    public float obstacleProbability = 0.1f;
	// Maximum number of obstacles to be present at a time
    public int maxObstacles = 12;
	// Probability of an enemy being generated per 100 milliseconds
    public float enemyProbability = 0.05f;
	// Maximum number of enemies to be present at a time
    public int maxEnemies = 2;
    public float powerupProbabiltiy = 0.01f;
    public int maxPowerUp = 2;
	// Material for the level
    public Material levelMaterial;

    public List<Element> obstacleList;
    public GameObject obstacleParent;
    public List<Element> powerUpList;
    public GameObject powerUpParent;
    public List<Element> enemyList;
    public GameObject enemyParent;

    private ElementManager obstacleManager;
    private ElementManager powerUpManager;
    private ElementManager enemyManager;

    // Initilizes the obstacle manager and calls the repeated function
    void Start () {
		// Wait two seconds, then call every 100 milliseconds
        InvokeRepeating("chooseElements", 2.0f, 0.1f);
        obstacleManager = gameObject.AddComponent<ElementManager>();
        obstacleManager.elements = obstacleList;
        obstacleManager.parent = obstacleParent;
        powerUpManager = gameObject.AddComponent<ElementManager>();
        powerUpManager.elements = powerUpList;
        powerUpManager.parent = powerUpParent;
        enemyManager = gameObject.AddComponent<ElementManager>();
        enemyManager.elements = enemyList;
        enemyManager.parent = enemyParent;
    }

	// Returns a random float between 0.0 and 1.0
    float getRand() {
        return Random.Range(0.0f, 1.0f);
    }

	// Decides, based on probabilities, when obstacles and enemies are to be generated
    void chooseElements() {
		// Get current number of active obstacles
		int obstacleCount = GameObject.FindGameObjectsWithTag("Obstacle").Length;
        int powerUpCount = GameObject.FindGameObjectsWithTag("PowerUp").Length;
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        // Check if obstacle is to be generated
        if ((getRand() <= obstacleProbability) && (obstacleCount <= maxObstacles)) {
			obstacleManager.generateElement();
        }
        if (getRand() <= powerupProbabiltiy && powerUpCount < maxPowerUp) {
            powerUpManager.generateElement();
        }
        // Check if enemy is to be generated
        if ((getRand() <= enemyProbability) && (enemyCount <= maxEnemies)) {
            enemyManager.generateElement();
        }
    }
}

