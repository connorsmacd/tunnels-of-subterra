/*
    File: ShipBulletCollision.cs
    Author: Connor S. MacDonald (B00632423)

    This file handles the collision between the ship's
    bullets and objects on the projectile collision layer mask
*/

using UnityEngine;
using System.Collections;

public class ShipBulletCollision : MonoBehaviour {
    // When a collision happens
    void OnCollisionEnter(Collision collision) {
        // Get collider
        Collider collider = collision.collider;
        // Check if dangler
        if (collider.tag == "Dangler") {
            // Damage dangler
            collider.GetComponent<DanglerStats>().damageDangler(5);
        // Check if swooper
        } else if (collider.tag == "Swooper") {
            // Damage swooper
            collider.GetComponent<SwooperStats>().damageSwooper(5);
        }
        // Destroy the projectile
        Destroy(gameObject);
    }
}
