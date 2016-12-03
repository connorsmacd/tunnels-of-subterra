using UnityEngine;
using System.Collections;

public class DanglerObstacleCollision : MonoBehaviour {

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Obstacle") {
            transform.parent.transform.GetComponent<DanglerStats>().killDangler();
        }
    }
}
