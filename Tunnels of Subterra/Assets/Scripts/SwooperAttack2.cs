using UnityEngine;
using System.Collections;

public class SwooperAttack2 : MonoBehaviour {
    // Distance away from ship that swooper will attack
    public float attackDistance = 25.0f;
    // Speed at which swooper searches
    public float searchSpeed = 50.0f;
    // Speed at which swooper moves side to side
    public float strafeSpeed = 3.0f;
    // Saves the previous strafe speed
    private float lastStrafeSpeed = 3.0f;
    // Speed at which swooper flies backwards
    private float hoverSpeed;
    // Enumerators for the states
    private enum states { searching, attacking }
    // Current state of swooper
    private int state = (int)states.searching;
    // Animation of swooper
    private Animation swooperAnimation;
    // Game object of ship
    private GameObject ship;
    // Transform of projectile emitter
    private Transform emitter;
    // Game object of projectile
    public GameObject projectile;
    public Texture texture;

    // Use this for initialization
    void Start() {
        // Get animation
        swooperAnimation = transform.GetComponent<Animation>();
        // Set hover speed
        hoverSpeed = -GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().levelSpeed;
        // Set ship
        ship = GameObject.FindGameObjectWithTag("Ship");
        // Get emitter
        emitter = transform.GetChild(0);
        // And some randomness to the attack distance
        attackDistance += Random.Range(-10, 0);
        // Change material
        transform.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = texture; 
    }

    // Update is called once per frame
    void Update() {
        // Switch on state
        switch (state) {
            case (int)states.searching:
                // Play animation
                swooperAnimation["Fly"].speed = 2.0f;
                swooperAnimation.Play("Fly");
                // Check if ship is close
                if ((transform.position.z - ship.transform.position.z) > attackDistance) {
                    // Keep searching
                    transform.Translate(0, 0, searchSpeed * Time.deltaTime);
                } else {
                    // Start attacking
                    state = (int)states.attacking;
                    // Get length of attack animation
                    float attackTime = swooperAnimation["Attack"].length;
                    // Start shooting
                    InvokeRepeating("Shoot", attackTime, attackTime);
                }
                break;
            case (int)states.attacking:
                // Fly backwards
                if (transform.position.x <= -4.0f) {
                    transform.Translate(-strafeSpeed * Time.deltaTime, 0, hoverSpeed * Time.deltaTime);
                    lastStrafeSpeed = -strafeSpeed;
                } else if (transform.position.x >= 4.0f) {
                    transform.Translate(strafeSpeed * Time.deltaTime, 0, hoverSpeed * Time.deltaTime);
                    lastStrafeSpeed = strafeSpeed;
                }  else
                    transform.Translate(lastStrafeSpeed * Time.deltaTime, 0, hoverSpeed * Time.deltaTime);
                // Update last strafe speed
                // Play attack animation
                swooperAnimation.Play("Attack");
                break;
        }
    }

    // Shoots at the ship
    private void Shoot() {
        // Point emitter at ship
        emitter.LookAt(ship.transform);
        // Instantiate projectile
        GameObject shotProjectile = (GameObject)Instantiate(projectile, emitter.position, emitter.rotation, emitter);
        // Add force to projectile in direction gun is pointing
        shotProjectile.GetComponent<Rigidbody>().AddForce(emitter.forward * 75, ForceMode.Impulse);
        // Destroy projectile after 5 seconds
        Destroy(shotProjectile, 5.0f);
    }
}
