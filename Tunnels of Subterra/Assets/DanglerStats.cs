using UnityEngine;
using System.Collections;

public class DanglerStats : MonoBehaviour {

	public float hitpoints = 10.0f;
    public int scoreValue = 50;
    public GameObject bloodEffect;
    
    public void damageDangler (float damage) {
        hitpoints -= damage;
    }

    public void killDangler () {
        hitpoints = 0.0f;
    }

    void Update () {
        if (hitpoints <= 0.0f) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().modifyScore(scoreValue);
            Instantiate(bloodEffect, null);
            gameObject.SetActive(false);
        }
    }
}
