using UnityEngine;
using System.Collections;

public class EnvironmentCollision : MonoBehaviour {

	void OnTriggerEnter (Collider collider) {
        if (collider.tag == "ShipBullet") {
            Destroy(collider.gameObject);
        }
    }
}
