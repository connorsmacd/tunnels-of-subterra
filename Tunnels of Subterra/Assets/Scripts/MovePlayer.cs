/*
    Script: MovePlayer.cs
    Author: Connor S. MacDonald (B00632423)

    Simply moves the player along the z axis at level speed
 */

using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {
    // Speed of the player
    public float playerSpeed { get; set; }

    // Called at start
    void Start() {
        // Set player speed to level speed
        playerSpeed = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().levelSpeed;
    }

    // Called every frame
    void Update() {
        // Translate player
        transform.Translate(0, 0, playerSpeed * Time.deltaTime);
    }
}
