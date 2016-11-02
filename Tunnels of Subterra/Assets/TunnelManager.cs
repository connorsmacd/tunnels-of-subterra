using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunnelManager : MonoBehaviour {

    public GameObject tunnelSegmentNormal;
    public GameObject tunnelSegmentReflected;
    public GameObject parent;
    public float distanceFromFog = 5.0f;
    private Queue<GameObject> segmentQueue = new Queue<GameObject>();
    private bool reflect;
    private float currentSegmentZ;
    private float segmentLength;

    void addNewSegment () {
        currentSegmentZ += segmentLength;
        Vector3 position = new Vector3(0, 0, currentSegmentZ);
        GameObject newSegment;
        Quaternion rotation = new Quaternion();
        if (reflect) {
            newSegment = (GameObject) Instantiate(tunnelSegmentNormal, position, rotation);
            reflect = false;
        } else {
            rotation = Quaternion.Euler(0, 0, 180);
            newSegment = (GameObject) Instantiate(tunnelSegmentReflected, position, rotation);
            reflect = true;
        }
        newSegment.transform.parent = parent.transform;
        segmentQueue.Enqueue(newSegment);
    }

    void removeOldSegment () {
        Destroy(segmentQueue.Dequeue());
    }

	void Start () {
        reflect = false;
        currentSegmentZ = 0.0f;
        GameObject firstSegment = (GameObject) Instantiate(tunnelSegmentNormal, new Vector3(0, currentSegmentZ, 0), new Quaternion());
        firstSegment.transform.parent = parent.transform;
        segmentQueue.Enqueue(firstSegment);
        segmentLength = segmentQueue.Peek().transform.GetChild(2).GetComponent<MeshRenderer>().bounds.size.z;
    }

	void Update () {
        float frontFogZ = GameObject.FindGameObjectWithTag("Player").transform.FindChild("FrontFog").transform.position.z;
        float rearFogZ = GameObject.FindGameObjectWithTag("Player").transform.FindChild("RearFog").transform.position.z;
        if ((frontFogZ - currentSegmentZ) >= ((segmentLength / 2) - distanceFromFog)) {
            addNewSegment();
        }
        if ((rearFogZ - segmentQueue.Peek().transform.position.z) >= (segmentLength / 2)) {
            removeOldSegment();
        }
	}
}
	