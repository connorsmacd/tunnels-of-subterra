using UnityEngine;
using System.Collections;

public class FollowCursor : MonoBehaviour {

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Ray aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = aimRay.GetPoint(Vector3.Distance(gameObject.transform.position, aimRay.origin));
        point.z = gameObject.transform.parent.transform.position.z;
        gameObject.transform.position = point;
    }
}
