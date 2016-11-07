using UnityEngine;
using System.Collections;

/*
 * Script: FollowCursor.cs
 * Author: Connor MacDonald (B00632423)
 *
 * This script makes the reticle follow the cursor
 */

public class FollowCursor : MonoBehaviour {

	void Update () {
        Ray aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = aimRay.GetPoint(Vector3.Distance(transform.position, aimRay.origin));
        point.z = transform.parent.transform.position.z;
        transform.position = point;
    }
}
