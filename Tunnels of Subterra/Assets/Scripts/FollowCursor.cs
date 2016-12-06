/*
    Script: FollowCursor.cs
    Author: Connor S. MacDonald (B00632423)

    This script the allows the reticle to follow
    the cursor.
 */

using UnityEngine;
using System.Collections;

public class FollowCursor : MonoBehaviour {

	void Update () {
        // Get ray of cursor at the camera
        Ray aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Find where ray is casted on the canvas
        Vector3 point = aimRay.GetPoint(Vector3.Distance(transform.position, aimRay.origin));
        // Set the z component to be on the canvas
        point.z = transform.parent.transform.position.z;
        // Set the transform
        transform.position = point;
    }
}
