using UnityEngine;
using System.Collections;

public class ShipBulletCollision : MonoBehaviour {

    void OnCollisionEnter(Collision collision) {
        Collider collider = collision.collider;
        if (collider.tag == "Dangler") {
            collider.GetComponent<DanglerStats>().damageDangler(5);
        } else if (collider.tag == "Swooper") {
            collider.GetComponent<SwooperStats>().damageSwooper(5);
        }
        Destroy(gameObject);
    }
}
