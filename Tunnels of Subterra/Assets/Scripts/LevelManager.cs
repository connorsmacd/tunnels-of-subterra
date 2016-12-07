/*
    Script: LevelManager.cs
    Author: Connor S. MacDonald (B00632423)
 
    This function determines the behaviour of a level and holds all
    of the information that is required for the level.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	// Speed that the ship will travel at
    public float levelSpeed = 100.0f;
    public string winScene = "Level";

    public string loseScene = "Level";
    // Probability of an obstacle being generated per 100 milliseconds
    public float obstacleProbability = 0.1f;
	// Maximum number of obstacles to be present at a time
    public int maxObstacles = 12;
	// Probability of an enemy being generated per 100 milliseconds
    public float enemyProbability = 0.05f;
	// Maximum number of enemies to be present at a time
    public int maxEnemies = 2;
    public float powerupProbability = 0.01f;
    public int maxPowerUp = 2;
	// Material for the level
    public Material levelMaterial;

    // List of obstacle elements
    public List<Element> obstacleList;
    // Parent to the obstacles
    public GameObject obstacleParent;
    // List of power up elements
    public List<Element> powerUpList;
    // Parent to the power ups
    public GameObject powerUpParent;
    // Lsit of enemy elements
    public List<Element> enemyList;
    // Parent to enemies
    public GameObject enemyParent;
    //Sets the max of the level, -1 for infinite
    public float maxLength = -1;
    //Sets the max of the time, -1 for infinite
    public float maxTime = -1;
    //Sets the max of the enemies killed, -1 for infinite
    public float maxEnemyKills = -1;
    private float timePassed = 0;
    private bool won = false;

    // Element managers
    private ElementManager obstacleManager;
    private ElementManager powerUpManager;
    private ElementManager enemyManager;
    private HUD hud;
    private TunnelManager tunnelManager;

    // Initilizes the obstacle manager and calls the repeated function
    void Start () {
        // Initialize all element managers
        obstacleManager = gameObject.AddComponent<ElementManager>();
        obstacleManager.elements = obstacleList;
        obstacleManager.parent = obstacleParent;
        powerUpManager = gameObject.AddComponent<ElementManager>();
        powerUpManager.elements = powerUpList;
        powerUpManager.parent = powerUpParent;
        enemyManager = gameObject.AddComponent<ElementManager>();
        enemyManager.elements = enemyList;
        enemyManager.parent = enemyParent;
        hud = GameObject.FindGameObjectWithTag("Canvas").GetComponent<HUD>();
        tunnelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TunnelManager>();
        // Wait two seconds, then call every 100 milliseconds
        InvokeRepeating("chooseElements", 2.0f, 0.1f);
    }

    void Update () {
        timePassed += Time.deltaTime;
        if (checkWin() && won == false)
        {
            startWin();
            won = true;
        }
    }

	// Returns a random float between 0.0 and 1.0
    float getRand() {
        return Random.Range(0.0f, 1.0f);
    }

	// Decides, based on probabilities, when obstacles and enemies are to be generated
    void chooseElements() {
		// Get current number of active elements
		int obstacleCount = GameObject.FindGameObjectsWithTag("Obstacle").Length;
        int powerUpCount = GameObject.FindGameObjectsWithTag("PowerUp").Length;
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        // Check if obstacle is to be generated
        if ((getRand() <= obstacleProbability) && (obstacleCount < maxObstacles)) {
			obstacleManager.generateElement();
        }
        // Check if power up is to be generated
        if ((getRand() <= powerupProbability) && (powerUpCount < maxPowerUp)) {
            powerUpManager.generateElement();
        }
        // Check if enemy is to be generated
        if ((getRand() <= enemyProbability) && (enemyCount < maxEnemies)) {
            enemyManager.generateElement();
        }
    }

    private bool checkWin()
    {
        if (tunnelManager.getTotalLength() > maxLength && maxLength >= 0)
        {
            return true;
        }
        if (timePassed > maxTime && maxTime >= 0)
        {
            return true;
        }
        /*if (tunnelManager.getTotalLength() > maxEnemyKills && maxEnemyKills >= 0)
        {
            return true;
        }
        dont have a way to keep track of this yet.
         */
        return false;
    }

    public void loadScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    void startWin()
    {
        //game logic to load next level
        Debug.Log("PlayerWin started");
        hud.playerWin();
        StartCoroutine(loadWin());

    }

    IEnumerator loadWin()
    {
        yield return new WaitForSecondsRealtime(5);
        loadScene(winScene);
    }

    public void lose_Scene()
    {
        loadScene(loseScene);
    }

    public void reload()
    {
        loadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}


