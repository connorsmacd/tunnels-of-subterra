using UnityEngine;
using System.Collections;

public class TunnelManager : MonoBehaviour { 

	public GameObject 
	private Vector3 cameraPosition;
	private	Renderer tunnelRenderer;

	void Start () {
		cameraPosition = GameObject.FindGameObjectWithTag("Fog")
											.transform.position;
		tunnelRenderer = GameObject.FindGameObjectWithTag("Tunnel")
											   .transform.position;
	}

	void Update () {
		if (
	}
}
	