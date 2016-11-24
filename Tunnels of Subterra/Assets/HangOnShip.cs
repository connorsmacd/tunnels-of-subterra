using UnityEngine;
using System.Collections;

public class HangOnShip : MonoBehaviour {

	void OnTriggerEnter(Collider collider) {
        if (collider.tag == "HangPoint") {
            transform.GetComponent<Collider>().isTrigger = false;
            Transform hangTransform = collider.transform.GetChild(0).transform;
            transform.parent.GetComponent<DanglerAttack1>().setHanging(hangTransform);
        }
    }
}
