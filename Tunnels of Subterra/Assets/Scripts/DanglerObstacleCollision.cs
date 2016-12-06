/*
    File: DanglerObstacleCollision.cs
    Author: Connor S. MacDonald (B00632423)

    Handles the collision between danglers and obstacles when
    hanging on ship.
*/

using UnityEngine;
using System.Collections;

public class DanglerObstacleCollision : MonoBehaviour {
    // Whe collides
    void OnTriggerEnter(Collider collider) {
        // Check if obstacle
        if (collider.tag == "Obstacle") {
            // Kill dangler
            transform.parent.transform.GetComponent<DanglerStats>().killDangler();
        }
    }
}
