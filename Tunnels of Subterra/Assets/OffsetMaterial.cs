using UnityEngine;
using System.Collections;

public class OffsetMaterial : MonoBehaviour {

    private float scrollSpeed;
    private Renderer rend;

    void Start() {
        rend = GetComponent<Renderer>();
        LevelManager lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        scrollSpeed = lm.levelSpeed / rend.material.mainTextureScale.y;
    }

    void Update() {
        rend.material.mainTextureOffset += Vector2.up * scrollSpeed * Time.deltaTime;
    }
}
