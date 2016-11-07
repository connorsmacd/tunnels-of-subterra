using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Script: ObstacleManager.cs
 * Author: Connor S. MacDonald (B00632423)
 * 
 * Manages the placement and deletion of obstacles.
 */
public class ObstacleManager : MonoBehaviour {

	// Structure to hold the information needed to place each obstacle
	[System.Serializable]
	public struct Obstacle {
		// Object of the obstacle
		public GameObject obstacleObject;
		// Rough x & y values that obstacle will be placed at
		public Vector2 roughCenter;
		// Orientation of the object when it is placed
		public Vector3 orientation;
		// Amount that the object can vary from roughCenter.x
		public float xVariance;
	}

	// A list of defined obstacles
	public List<Obstacle> obstacles;
	// Distance from front fog that obstacles are generated at
    public float distanceFromFog = 5.0f;
	// Parent of active obstacles
    public GameObject parent;

	// Queue of obstacles
    private Queue<GameObject> obstacleObjectQueue = new Queue<GameObject>();

	// Generates an obstacle when it is called
    public void generateObstacle() {
		// Choose an obstacle randomly from the list
		Obstacle currentObstacle = obstacles[Random.Range(0, obstacles.Count)];
		// Sanity check
		if (currentObstacle.obstacleObject != null) {
			// Get Z position of front fog
            float frontFogZ = GameObject.FindGameObjectWithTag("Player")
							  .transform.FindChild("FrontFog").transform.position.z;
			// Determine position of obstacle
            Vector3 position = (Vector3) currentObstacle.roughCenter + 
							   new Vector3(Random.Range(-currentObstacle.xVariance, currentObstacle.xVariance), 
										   0, frontFogZ + distanceFromFog);
			// Instantiate obstacle
            GameObject currentObstacleObject = (GameObject) Instantiate(currentObstacle.obstacleObject, position, 
																		Quaternion.Euler(currentObstacle.orientation));
			// Set parent
            currentObstacleObject.transform.parent = parent.transform;
			// Enqueue object
            obstacleObjectQueue.Enqueue(currentObstacleObject);
        }
	}

	// Checks if obstacles need to be destroyed
    void Update () {
		// Get Z position of rear fog
        float rearFogZ = GameObject.FindGameObjectWithTag("Player")
						 .transform.FindChild("RearFog").transform.position.z;
		// Check if objects are in queue
        if (obstacleObjectQueue.Count > 0) {
			// See if object is past rear fog
            if ((rearFogZ - obstacleObjectQueue.Peek().transform.position.z) >= 0) {
				// Destroy object at front of queue
                Destroy(obstacleObjectQueue.Dequeue());
            }
        }
    }
}
