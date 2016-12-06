/*
    File: HangOnShip.cs
    Author: Connor S. MacDonald (B00632423)

    This file handles the collision between the dangler's
    grab collider and the ship's hang-point collider, such
    that the dangler can be positioned correctly on the ship
*/

using UnityEngine;
using System.Collections;

public class HangOnShip : MonoBehaviour {
    // When a collision is detected
	void OnTriggerEnter(Collider collider) {
        // Check if collider is a hang point
        if (collider.tag == "HangPoint") {
            // Disable the collider's trigger
            transform.GetComponent<Collider>().isTrigger = false;
            // Get the transform of the hang point
            Transform hangTransform = collider.transform.GetChild(0).transform;
            // Set transform to transform of hang point
            transform.parent.GetComponent<DanglerAttack1>().beginHanging(hangTransform);
        }
    }
}
