using UnityEngine;
using System.Collections;

/*
 * Script: MovePlayer.cs
 * Author: Connor S. MacDonald (B00632423)
 * 
 * Simply moves the player along the z axis at level speed
 */
public class MovePlayer : MonoBehaviour {

    private float playerSpeed;

    void Start() {
        playerSpeed = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().levelSpeed;
    }

    void Update() {
        transform.Translate(0, 0, playerSpeed * Time.deltaTime);
    }
}
