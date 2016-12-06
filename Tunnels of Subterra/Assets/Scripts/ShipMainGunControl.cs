/*
    File: ShipMainGunControl.cs
    Author: Connor S. MacDonald (B00632423)

    This script controls the main ship weapon
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipMainGunControl : MonoBehaviour {
	// Fire rate of gun
    public float fireRate = 0.5f;
    // Game object for the projectile
    public GameObject projectile;
    // Parent of projectile
    public GameObject projectileParent;

    // Called every frame
	void Update () {
        // Get cursor ray at camera
        Ray aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Set layer mask to collide only with the "Projectile Collision" layer
        int layerMask = 1 << 8;
        // RaycastHit object
        RaycastHit hitInfo;
        // Check if ray hit something
        if (Physics.Raycast(aimRay, out hitInfo, Mathf.Infinity, layerMask)) {
            // Point projectile emitter at target
            transform.LookAt(hitInfo.point, Vector3.up);
            // Check if target is enemy
            if ((hitInfo.collider.tag == "Dangler") || (hitInfo.collider.tag == "Swooper")) {
                // Set cursor to red
                GameObject.FindGameObjectWithTag("Cursor").GetComponent<Image>().color = Color.red;
            } else {
                // Set cursor to cyan
                GameObject.FindGameObjectWithTag("Cursor").GetComponent<Image>().color = Color.cyan;
            }
            // Draw a line from gun to target
            Debug.DrawLine(transform.position, hitInfo.point);
        }

        // Check if left mouse button is down
        if (Input.GetMouseButtonDown(0)) {
            // Start shooting
            InvokeRepeating("Shoot", 0.0f, fireRate);
        // Check if mouse button is up
        } else if (Input.GetMouseButtonUp(0)) {
            // Stop shooting
            CancelInvoke();
        }
    }

    // Invoked to shoot projectiles at fire rate
    private void Shoot () {
        // Intantiate projectile
        GameObject shotProjectile = (GameObject) Instantiate(projectile, transform.position, 
                                                             transform.rotation, projectileParent.transform);
        // Add force to projectile in direction gun is pointing
        shotProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 500, ForceMode.Impulse);
        // Destroy projectile after 5 seconds
        Destroy(shotProjectile, 5.0f);
        // Play shooting sound effect
        transform.GetComponent<AudioSource>().Play();
    }
}
