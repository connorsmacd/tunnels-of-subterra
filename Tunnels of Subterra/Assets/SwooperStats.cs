using UnityEngine;
using System.Collections;

public class SwooperStats : MonoBehaviour {

    public float hitpoints = 20.0f;
    public int scoreValue = 50;
    public GameObject bloodEffect;

    public void damageSwooper (float damage) {
        hitpoints -= damage;
    }

    public void killSwooper () {
        hitpoints = 0.0f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>().modifyScore(scoreValue);
        GameObject blood = (GameObject) Instantiate(bloodEffect, transform.position, new Quaternion());
        blood.GetComponent<ParticleSystem>().Play();
        Destroy(blood, 3.0f);
        gameObject.SetActive(false);
    }

    void FixedUpdate() {
        if (hitpoints <= 0.0f) {
            killSwooper();
        }
    }
}
