/*
    File: Element.cs
    Author: Connor S. MacDonald (B00632423)

    This file defines the element struct, which is used to store
    the game object of the element along with parameters governing
    how the element is instantiated.
*/

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
