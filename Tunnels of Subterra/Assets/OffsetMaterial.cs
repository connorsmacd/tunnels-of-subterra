using UnityEngine;
using System.Collections;

public class OffsetMaterial : MonoBehaviour {

    private float scrollSpeed;
    private Renderer rend;

    void Start() {
        rend = GetComponent<Renderer>();
        scrollSpeed = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().levelSpeed / 20;
    }

    void Update() {
        rend.material.mainTextureOffset += Vector2.up * scrollSpeed * Time.deltaTime;
    }
}
