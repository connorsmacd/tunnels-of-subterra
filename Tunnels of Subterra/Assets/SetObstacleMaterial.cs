using UnityEngine;
using System.Collections;

public class SetObstacleMaterial : MonoBehaviour {

	void Update () {
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        Material material = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().levelMaterial;
        for (int i = 0; i < meshRenderers.Length; i++) {
            meshRenderers[i].material = material;
        }
	}
}
