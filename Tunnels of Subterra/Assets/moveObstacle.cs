using UnityEngine;
using System.Collections;

public class moveObstacle : MonoBehaviour {

    private float obstacleSpeed;
    private float zOrigin;
    public float dist = 28.0f;

	// Use this for initialization
	void Start () {
        obstacleSpeed = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().levelSpeed;
        zOrigin = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Mathf.Abs(zOrigin - transform.position.z) < 28.0f) {
            transform.Translate(0, 0, obstacleSpeed * Time.deltaTime);
        } else {
            Destroy(gameObject);
        }
	}
}
