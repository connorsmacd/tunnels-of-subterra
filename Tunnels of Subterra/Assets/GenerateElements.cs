using UnityEngine;
using System.Collections;

public class GenerateElements : MonoBehaviour {

    public float levelSpeed = 1.5f;
    public int maxObstacles = 3;
    public int maxEnemies = 2;


	void Start () {
        StartCoroutine(chooseElements());
	}

    IEnumerator chooseElements() {
        while(true) {
            if (getRand() < 0.1f) {
                //generateObstacle();
            }
            if (getRand() < 0.05f) {
                //generateEnemy();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    float getRand() {
        return Random.Range(0.0f, 0.1f);
    }
}
