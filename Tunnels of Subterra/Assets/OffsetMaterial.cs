using UnityEngine;
using System.Collections;

public class OffsetMaterial : MonoBehaviour {

    public float scrollSpeed = 0.5F;
    public Renderer rend;
    void Start() {
        rend = GetComponent<Renderer>();
    }
    void Update() {
        float offset = Time.time * scrollSpeed;
        rend.material.mainTextureOffset = new Vector3(0, offset, 0);
    }
}
