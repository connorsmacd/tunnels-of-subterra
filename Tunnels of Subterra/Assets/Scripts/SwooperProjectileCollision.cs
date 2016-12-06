/*
    File: SwooperProjectileCollision.cs
    Author: Connor S. MacDonald (B00632423)

    This script handles the collision between swooper projectiles
    and the ship.
*/

using UnityEngine;
using System.Collections;

public class SwooperProjectileCollision : MonoBehaviour {

    void Start () {
        transform.GetComponent<AudioSource>().Play();
    }

    void OnCollisionEnter(Collision collision) {
        // Get collider
        Collider shipCollider = collision.collider;
        // Damage player
        shipCollider.transform.parent.GetComponent<PlayerCharacter>().doDamage(5);
        // Play hit sound
        shipCollider.GetComponent<AudioSource>().Play();
        // Destroy projectile
        if (gameObject != null) Destroy(gameObject);
    }
}
