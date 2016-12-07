/*
    File: SwooperStats.cs
    Author: Connor S. MacDonald (B00632423)

    This scripts holds the stats of a swooper
*/

using UnityEngine;
using System.Collections;

public class SwooperStats : MonoBehaviour {
    // Current hitpoints
    public float hitpoints = 20.0f;
    // Score value that swooper is worth
    public int scoreValue = 50;
    // Blood effect that is played when killed
    public GameObject bloodEffect;
    private LevelManager lvlman;

    // Damages the swooper
    public void damageSwooper (float damage) {
        hitpoints -= damage;
    }

    

    // Kills the swooper
    public void killSwooper () {
        // Set hitpoints to zero
        hitpoints = 0.0f;
        // Add to players score
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().modifyScore(scoreValue);
        // Instatiate blood
        GameObject blood = (GameObject) Instantiate(bloodEffect, transform.position, new Quaternion());
        // Play blood particle system
        blood.GetComponent<ParticleSystem>().Play();
        // Destroy blood after 3 seconds
        Destroy(blood, 3.0f);
        // Disable swooper
        gameObject.SetActive(false);
        lvlman.enemyDeath();
    }

    void start()
    {
        lvlman = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    // Fixed update for rigid body
    void FixedUpdate() {
        // Check if hitpoints are less than zero
        if (hitpoints <= 0.0f) {
            // Kill the swooper
            killSwooper();
        }
    }
}
