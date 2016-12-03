using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct Element {
    // Object of the obstacle
    public GameObject elementObject;
    // Rough x & y values that obstacle will be placed at
    public Vector2 roughCenter;
    // Orientation of the object when it is placed
    public Vector3 orientation;
    // Amount that the object can vary from roughCenter.x
    public float xVariance;
}
