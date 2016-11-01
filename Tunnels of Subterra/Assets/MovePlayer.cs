using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    private float playerSpeed;

    void Start() {
        playerSpeed = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().levelSpeed;
    }

    void Update() {
        transform.Translate(0, 0, playerSpeed * Time.deltaTime);
    }
}
