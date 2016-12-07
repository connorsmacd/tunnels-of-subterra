/*
    File: DanglerStats.cs
    Author: Connor S. MacDonald

    This scipt holds all of the states for a dangler
*/

using UnityEngine;
using System.Collections;

public class DanglerStats : MonoBehaviour {
    // Hit points of dangler
	public float hitpoints = 10.0f;
    // Score dangler is worth
    public int scoreValue = 50;
    // Game object for blood effect
    public GameObject bloodEffect;
    private LevelManager lvlman;

    // Damage dangker
    public void damageDangler (float damage) {
        hitpoints -= damage;
    }

    // Kills the dangler
    public void killDangler () {
        // Set hp to 0
        hitpoints = 0.0f;
        // Get player object
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().modifyScore(scoreValue);
        // Instantiate blood
        GameObject blood = (GameObject)Instantiate(bloodEffect, transform.position, new Quaternion());
        // Play blood animation
        blood.GetComponent<ParticleSystem>().Play();
        // Destroy blood after 3 seconds
        Destroy(blood, 3.0f);
        // Set inactive
        gameObject.SetActive(false);
        // Fling dangler
        transform.GetComponent<DanglerAttack1>().beginFlinging();
        lvlman.enemyDeath();
    }

    void start()
    {
        lvlman = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    void Update () {
        // Check if hitpoints are 0 or less
        if (hitpoints <= 0.0f) {
            // Kill dangler
            killDangler();
        }
    }
}
