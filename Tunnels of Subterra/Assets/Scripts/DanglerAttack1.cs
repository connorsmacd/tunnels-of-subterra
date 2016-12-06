/*
    File: DanglerAttack1.cs
    Author: Connor S. MacDonald (B00632423)

    This script controls the behaviour of the dangler
*/

using UnityEngine;
using System.Collections;

public class DanglerAttack1 : MonoBehaviour {
    // Distance from the ship when pounce happens
    public float pounceProximity = 4.0f;
    // Acceleration due to gravity
    public float gravity = 8.0f;
    // Max delta speed before dangler is flung off ship
    public float maxDeltaSpeed = 8.0f;

    // Translation vector of dangler
    private Vector3 translationVector = Vector3.zero;
    // Animation of dangler
    private Animation danglerAnimation;
    // Game object of ship
    private GameObject ship;
    // Ship controller object
    private ShipController shipController;
    // Enums for states
    private enum states { idle, pouncing, hanging, flung };
    // Current state
    private int state = (int) states.idle;

	// Use this for initialization
	void Start () {
        // Get animation
        danglerAnimation = gameObject.GetComponent<Animation>();
        // Get ship
        ship = GameObject.FindGameObjectWithTag("Ship");
        // Get ship controller
        shipController = ship.transform.GetComponent<ShipController>();
    }
	
	// Update is called once per frame
	void Update () {
        // Switch on state
        switch (state) {
            case (int) states.idle:
                // Get bounds of ship
                Bounds shipBounds = ship.GetComponent<MeshRenderer>().bounds;
                // Determine where front of ship is
                Vector3 shipFront = shipBounds.center + new Vector3(0, 0, shipBounds.extents.z);
                // Check if ship is close
                if (transform.position.z - shipFront.z > pounceProximity) {
                    danglerAnimation.Play("Idle");
                } else {
                    // Switch state
                    state = (int) states.pouncing;
                    // Calculate translation vector to pounce at the ship
                    // Get speed of ship
                    float shipSpeed = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().levelSpeed;
                    // Determine time ship will take to get to dangler
                    float shipTravelTime = pounceProximity / shipSpeed;
                    // Get position of hang collider
                    Vector3 colliderPosition = transform.GetChild(0).transform.position;
                    // Determine z velocity
                    translationVector.z = -(shipFront.x - colliderPosition.x) / shipTravelTime;
                    // Determine y acceleration
                    translationVector.y = ((shipFront.y - colliderPosition.y) 
                                            - (0.5f * -gravity * shipTravelTime * shipTravelTime)) / shipTravelTime;
                }
                break;
            case (int) states.pouncing:
                // Play pounce animation
                danglerAnimation["pounce"].speed = 2f;
                danglerAnimation.Play("pounce");
                // Decellerate
                translationVector.y -= gravity * Time.deltaTime;
                // Translate
                transform.Translate(translationVector * Time.deltaTime);
                // Check if missed ship
                if (transform.position.z < ship.transform.position.z)
                    // Switch state
                    state = (int) states.flung;
                break;
            case (int) states.hanging:
                // Play attack animation
                danglerAnimation.Play("attack");
                // Check if shaken off
                if ((Mathf.Abs(shipController.xDeltaSpeed) > maxDeltaSpeed) ||
                    (Mathf.Abs(shipController.yDeltaSpeed) > maxDeltaSpeed)) {
                    beginFlinging();
                }
                break;
            case (int) states.flung:
                // Play flung animation
                danglerAnimation["pounce"].speed = 2f;
                danglerAnimation.Play("pounce");
                // Decellerate
                translationVector.y -= gravity * Time.deltaTime;
                // Translate
                transform.Translate(translationVector * Time.deltaTime);
                break;
        }
    }

    // Called when collision with ship happens
    public void beginHanging (Transform hangTransform) {
        if (state != (int)states.hanging) {
            // Switch state
            state = (int)states.hanging;
            // Preserve x component
            Vector3 xOriginal = new Vector3(transform.position.x,
                                            hangTransform.position.y,
                                            hangTransform.position.z);
            // Set parent
            transform.parent = hangTransform;
            // Orient properly
            transform.rotation = hangTransform.rotation;
            // Set position
            transform.position = xOriginal;
            // Get length of animation
            float attackTime = danglerAnimation["attack"].length;
            // Disable collider
            transform.GetComponent<Collider>().enabled = false;
            // Start attacking
            InvokeRepeating("damageShip", attackTime, attackTime);
        } 
    }

    // Flings dangler
    public void beginFlinging () {
        // Detach from parent
        transform.parent = null;
        // Set rotation
        transform.rotation = Quaternion.Euler(0, -90, 0);
        // Set translation
        translationVector.y = shipController.ySpeedLast;
        translationVector.z = shipController.xSpeedLast;
        // Stop attacking
        CancelInvoke();
        // Switch state
        state = (int) states.flung;
    }

    // Damages the ship
    void damageShip () {
        // Play sound effect
        transform.GetComponent<AudioSource>().Play();
        // Damage ship
        ship.transform.parent.GetComponent<PlayerCharacter>().doDamage(3);
    }
}

