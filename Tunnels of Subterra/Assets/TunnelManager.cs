using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TunnelManager : MonoBehaviour {

    public GameObject tunnelObjectNormal;
    public GameObject tunnelObjectReflected;
    private Queue<GameObject> segmentQueue = new Queue<GameObject>();
    private bool reflect;
	private float frontFogZ;
    private float rearFogZ;
    private float currentSegmentZ;
    private float segmentLength;

    void addNewSegment() {
        currentSegmentZ += segmentLength;
        Vector3 position = new Vector3(0, 0, currentSegmentZ);
        GameObject newSegment;
        if (reflect) {
            newSegment = tunnelObjectNormal;
            reflect = false;
        } else {
            newSegment = tunnelObjectReflected;
            reflect = true;
        }
        Instantiate(newSegment, position, new Quaternion());
    }

	void Start () {
        Instantiate(tunnelObjectNormal, new Vector3(0, 0, 0), new Quaternion());
        segmentQueue.Enqueue(tunnelObjectNormal);
        frontFogZ = GameObject.FindGameObjectWithTag("Player").transform.FindChild("FrontFog").transform.position.z;
        rearFogZ = GameObject.FindGameObjectWithTag("Player").transform.FindChild("RearFog").transform.position.z;
        reflect = false;
        currentSegmentZ = 0.0f;
        segmentLength = segmentQueue.Peek().transform.GetChild(2).GetComponent<MeshRenderer>().bounds.size.y;
    }

	void Update () {
        frontFogZ = GameObject.FindGameObjectWithTag("Player").transform.FindChild("FrontFog").transform.position.z;
        if ((frontFogZ - currentSegmentZ) >= (segmentLength / 2)) {
            addNewSegment();
        }
	}
}
	