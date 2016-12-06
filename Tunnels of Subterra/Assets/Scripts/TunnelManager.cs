/*
    Script: TunnelManager.cs
    Author: Connor S. MacDonald (B00632423)

    This script controls the placement of the tunnel.
    The tunnel is broken up into segments that are repeated
    in front of each other as the player moves. As well, old
    segments are destroyed as the player moves a sufficient distance
    away.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunnelManager : MonoBehaviour {

	// The standard tunnel segment
    public GameObject tunnelSegmentNormal;
	// The reflected version of the tunnel segment
    public GameObject tunnelSegmentReflected;
	// The parent given to the tunnel segments
    public GameObject parent;
	// The distance the fog is from the end of the tunnel
	// before a new segment is generated
    public float distanceFromFog = 5.0f;
	// A queue for all of the tunnel segments
    private Queue<GameObject> segmentQueue = new Queue<GameObject>();
	// Whether or not the current segment is reflected
    private bool reflect;
	// Z position of the current segment
    private float currentSegmentZ;
	// Length of the segments
    private float segmentLength;
    // keeps track of how long the level is
    private float totalLength = 0;
    //Sets the max of the level, -1 for infinite
    private float maxLength = -1;

	// Adds a new segment in front of the most recent segment
    void addNewSegment () {
		// Get new z position
        currentSegmentZ += segmentLength;
        totalLength += segmentLength;
		// Make position vector for the segment
        Vector3 position = new Vector3(0, 0, currentSegmentZ);
        GameObject newSegment;
        Quaternion rotation = new Quaternion();
		// Check if current segment is reflected
        if (reflect) {
			// Instantiate new segment as unreflected
            newSegment = (GameObject) Instantiate(tunnelSegmentNormal, position, rotation);
            reflect = false;
        } else {
			// Instantiate new segment as reflected with proper orientation
            rotation = Quaternion.Euler(0, 0, 180);
            newSegment = (GameObject) Instantiate(tunnelSegmentReflected, position, rotation);
            reflect = true;
        }
		// Set parent
        newSegment.transform.parent = parent.transform;
		// Enqueue the segment
        segmentQueue.Enqueue(newSegment);
    }

	// Destroys the segment at the top of the queue
    void removeOldSegment () {
        Destroy(segmentQueue.Dequeue());
    }

	// Initializes everything
	void Start () {
        reflect = false;
        currentSegmentZ = 0.0f;
		// Instantiate first segment
        GameObject firstSegment = (GameObject) Instantiate(tunnelSegmentNormal, new Vector3(0, currentSegmentZ, 0), new Quaternion());
		// Set parent
        firstSegment.transform.parent = parent.transform;
		// Enqueue first segment
        segmentQueue.Enqueue(firstSegment);
		// Get segment length
        segmentLength = segmentQueue.Peek().transform.GetChild(2).GetComponent<MeshRenderer>().bounds.size.z;
    }

	// Checks if new segments need to be generated or old segments need to be destroyed
	void Update () {
		// Get Z position of the front fog
        float frontFogZ = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Front Fog").transform.position.z;
		// Get Z position of the rear fog
        float rearFogZ = GameObject.FindGameObjectWithTag("Player").transform.FindChild("Rear Fog").transform.position.z;
		// Check if new segment needs to be generated
        if ((frontFogZ - currentSegmentZ) >= ((segmentLength / 2) - distanceFromFog)) {
            addNewSegment();
        }
		// Check if old segment needs to be deleted
        if ((rearFogZ - segmentQueue.Peek().transform.position.z) >= (segmentLength / 2)) {
            removeOldSegment();
        }
	}
}
