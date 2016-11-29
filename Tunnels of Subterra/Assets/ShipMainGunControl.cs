using UnityEngine;
using System.Collections;

public class ShipMainGunControl : MonoBehaviour {
	
    public float fireRate = 0.5f;
    public GameObject projectile;

	void FixedUpdate () {
        Ray aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 8;
        RaycastHit hitInfo;
        if (Physics.Raycast(aimRay, out hitInfo, Mathf.Infinity, layerMask)) {
            transform.LookAt(hitInfo.point, Vector3.up);
            Debug.DrawLine(transform.position, hitInfo.point);
        }

        if (Input.GetMouseButtonDown(0)) {
            InvokeRepeating("Shoot", 0.0f, fireRate);
        } else if (Input.GetMouseButtonUp(0)) {
            CancelInvoke();
        }
    }

    private void Shoot () {
        GameObject shotProjectile = (GameObject) Instantiate(projectile, transform.position, transform.rotation);
        Destroy(shotProjectile, 1.0f);
    }
}
