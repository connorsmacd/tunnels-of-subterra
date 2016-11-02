using UnityEngine;
using System.Collections;

/*
 * Script: SetObstacleMaterial
 * Author: Connor MacDonald
 * 
 * Sets the material of all obstacles to the level material
 */
public class SetObstacleMaterial : MonoBehaviour {

	void Update () {
		// Get list of mesh renderers
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
		// Get level material
        Material material = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().levelMaterial;
		// Set material of all mesh renderers to level material
        for (int i = 0; i < meshRenderers.Length; i++) {
            meshRenderers[i].material = material;
        }
	}
}
