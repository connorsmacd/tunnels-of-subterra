using UnityEngine;
using System.Collections;

public class moveObstacle : MonoBehaviour {

    private float obstacleSpeed;
    private float zOrigin;
    public float dist = 70.0f;

	void Start () {
        obstacleSpeed = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().levelSpeed / 5;
        zOrigin = transform.position.z;
	}
	
	void Update () {
	    if (Mathf.Abs(zOrigin - transform.position.z) < dist) {
            transform.Translate(0, 0, -obstacleSpeed * Time.deltaTime);
        } else {
            Destroy(gameObject);
        }
	}
}
