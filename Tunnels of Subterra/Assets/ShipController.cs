using UnityEngine;
using System.Collections;

/*
 * Script: ShipController.cs
 * Author: Cole W. DeMan (B00602412)
 * 
 * This script controls the ship.
 */

public class ShipController : MonoBehaviour {

    //bounds on the amount the ship can move left/right & up/down (cheaper than collison detection)
    public float speed = 10;
    public float ceiling = 10;
    public float floor = 10;
    public float left = 10;
    public float right = 10;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        //the following keeps the ship within bounds, badly. (I'll fix this but for now its works well enough)
        Vector3 position = transform.localPosition;
        float x_pos = position.x;
        float y_pos = position.y;
        Debug.Log("X: "+x_pos);
        Debug.Log("y: "+y_pos);
        if (x_pos + x > left && x_pos + x <right){
            transform.Translate(x, 0, 0);
        }
        else
        {
            transform.Translate(x * -1, 0, 0); 
        }
        if (y_pos > floor && y_pos < ceiling)
        {
            transform.Translate(0, 0, z);
        }
        else
        {
            transform.Translate(0, 0, (z*-1)*2);
        }
    }
}
