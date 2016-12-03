using UnityEngine;
using System.Collections;

public class ShieldPowerUp : MonoBehaviour {

    private GameObject player;

    public float shieldFor = 25.0f;
    public int scoreValue = 500;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("shield trigger entered.");

        if (other.transform.parent.tag == player.tag)
        {
            player.GetComponent<PlayerCharacter>().addShield(shieldFor);
            player.GetComponent<PlayerCharacter>().modifyScore(scoreValue);
            Debug.Log("Player hit shield powerup.");
        }

    }
}
