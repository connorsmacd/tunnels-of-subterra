using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour {

    public float projectileSpeed = 1000.0f;
	
	void Update () {
        transform.Translate(transform.forward * projectileSpeed * Time.deltaTime);
	}
}
