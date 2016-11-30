using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipMainGunControl : MonoBehaviour {
	
    public float fireRate = 0.5f;
    public GameObject projectile;
    public GameObject projectileParent;

	void Update () {
        Ray aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 8;
        RaycastHit hitInfo;
        if (Physics.Raycast(aimRay, out hitInfo, Mathf.Infinity, layerMask)) {
            transform.LookAt(hitInfo.point, Vector3.up);
            if ((hitInfo.collider.tag == "Dangler") || (hitInfo.collider.tag == "Swooper")) {
                GameObject.FindGameObjectWithTag("Cursor").GetComponent<Image>().color = Color.red;
            } else {
                GameObject.FindGameObjectWithTag("Cursor").GetComponent<Image>().color = Color.cyan;
            }
            Debug.DrawLine(transform.position, hitInfo.point);
        }

        if (Input.GetMouseButtonDown(0)) {
            InvokeRepeating("Shoot", 0.0f, fireRate);
        } else if (Input.GetMouseButtonUp(0)) {
            CancelInvoke();
        }
    }

    private void Shoot () {
        GameObject shotProjectile = (GameObject) Instantiate(projectile, transform.position, 
                                                             transform.rotation, projectileParent.transform);
        shotProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 500, ForceMode.Impulse);
        Destroy(shotProjectile, 5.0f);
    }
}
