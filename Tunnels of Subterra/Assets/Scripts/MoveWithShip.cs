/*
    File: MoveWithSup.cs
    Author: Connor S. MacDonald

    This script moves the swooper projectile collider
    object with the ship. Said object cannot be a child of the
    ship.
*/

using UnityEngine;
using System.Collections;

public class MoveWithShip : MonoBehaviour {

    private GameObject ship;

    void Start () {
        // Get ship
        ship = GameObject.FindGameObjectWithTag("Ship");
    }

    // Update is called once per frame
    void Update() {
        transform.position = ship.transform.position;
    }
}
