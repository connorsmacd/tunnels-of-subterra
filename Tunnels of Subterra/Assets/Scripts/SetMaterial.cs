/*
    Script: SetMaterial.cs
    Author: Connor S. MacDonald (B00632423)

    Sets the material of all mesh renderers of the tunnel to the level material
 */

using UnityEngine;
using System.Collections;

public class SetMaterial : MonoBehaviour {

	void Start () {
		// Get all mesh renderers
        MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
		// Get level material
        Material material = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().levelMaterial;
		// Set all mesh renderers' materials to level material
        for (int i = 0; i < meshRenderers.Length; i++) {
            meshRenderers[i].material = material;
        }
	}
}
