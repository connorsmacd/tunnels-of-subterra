using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour {

	[System.Serializable]
	public struct Obstacle {
		public GameObject obstacleObject;
		public Vector2 roughPosition;
		public Vector3 orientation;
		public float xVariance;
	}

	public List<Obstacle> obstacles; 
    public float distanceFromFog = 5.0f;
    public GameObject parent;

    private Queue<GameObject> obstacleObjectQueue = new Queue<GameObject>();

    public void generateObstacle() {
		Obstacle currentObstacle = obstacles[Random.Range(0, obstacles.Count)];
		if (currentObstacle.obstacleObject != null) {
            float frontFogZ = GameObject.FindGameObjectWithTag("Player").transform.FindChild("FrontFog").transform.position.z;
            Vector3 position = (Vector3) currentObstacle.roughPosition 
                               + new Vector3(Random.Range(-currentObstacle.xVariance, currentObstacle.xVariance), 0, frontFogZ + distanceFromFog);
            GameObject currentObstacleObject = (GameObject) Instantiate(currentObstacle.obstacleObject, 
                                                                        position, Quaternion.Euler(currentObstacle.orientation));
            currentObstacleObject.transform.parent = parent.transform;
            obstacleObjectQueue.Enqueue(currentObstacleObject);
        }
	}

    void Update () {
        float rearFogZ = GameObject.FindGameObjectWithTag("Player").transform.FindChild("RearFog").transform.position.z;
        if (obstacleObjectQueue.Count > 0) {
            if ((rearFogZ - obstacleObjectQueue.Peek().transform.position.z) >= 0) {
                Destroy(obstacleObjectQueue.Dequeue());
            }
        }
    }
}
