using UnityEngine;
using System.Collections;

public class ShipMainGunControl : MonoBehaviour {
	
    public float fireRate = 0.5f;
    public float projectileForce = 1000;
    public GameObject projectile;

    private bool right = true;

	void Update () {
        Ray aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 8;
        RaycastHit hitInfo;
        if (Physics.Raycast(aimRay, out hitInfo, Mathf.Infinity, layerMask)) {
            transform.LookAt(hitInfo.point, Vector3.up);
        }

        if (Input.GetMouseButton(0)) {
            InvokeRepeating("Shoot", 0.0f, fireRate);
        } else {
            CancelInvoke();
        }
    }

    private void Shoot () {
        GameObject shotProjectile = (GameObject) Instantiate(projectile, transform.position, transform.rotation);
        shotProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileForce);
        Destroy(shotProjectile, 10.0f);
    }
}
