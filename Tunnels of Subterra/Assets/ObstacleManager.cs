using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour {

	[System.Serializable]
	public class Obstacle {

		public GameObject obstacleObject;
		public Vector3 roughPosition;
		public Vector3 orientation;
		public float xVariance;

		public Obstacle(GameObject obstacleObject, Vector3 roughPosition, 
						float xVariance, Vector3 orientation) {
			this.obstacleObject = obstacleObject;
			this.roughPosition = roughPosition;
			this.xVariance = xVariance;
			this.orientation = orientation;
		}

		public void placeObstacle() {
			Vector3 pos = roughPosition + new Vector3(Random.Range(-xVariance, xVariance), 0, 0);
			Instantiate(obstacleObject, pos, Quaternion.Euler(orientation));
		}
	}

	public List<Obstacle> obstacles;

	public void generateObstacle() {
		Obstacle currentObstacle = obstacles[Random.Range(0, obstacles.Count)];
		if (currentObstacle.obstacleObject != null)
			currentObstacle.placeObstacle();
	}
}
