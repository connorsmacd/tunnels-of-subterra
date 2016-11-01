using UnityEngine;
using System.Collections;

public class SetMaterial : MonoBehaviour {

	void Start () {
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        Material material = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().levelMaterial;
        for (int i = 0; i < meshRenderers.Length; i++) {
            meshRenderers[i].material = material;
        }
	}
}
