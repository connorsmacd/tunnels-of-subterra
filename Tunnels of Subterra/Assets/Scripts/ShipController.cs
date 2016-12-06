/*
    Script: ShipController.cs
    Author: Connor MacDonald (B00632423)
            Cole W. DeMan (B00602412)
            Mike McPhee

    This script controls the horizontal and vertical movement of the ship,
    as well as the collisions of the ship with obstacles
*/

using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {
    // How nimble the ship is
    public float nimbilityFactor = 0.10f;
    // Max horizontal and veritcal speed
    public float maxSpeed = 10f;
    // Amount the ship is offset by the cursors location in the y axis
    public float cursorOffset = -0.5f;

    // X and y last frame's change in speed
    public float xDeltaSpeed { get; set; }
    public float yDeltaSpeed { get; set; }
    // X and y last frame's speed
    public float xSpeedLast { get; set; }
    public float ySpeedLast { get; set; }

    // Player object
    private GameObject player;
    // X and y extents of the tunnels bounds
    private Vector2 boundsExtents;

    // Use this for initialization
    void Start () {
        // Get tunnel bounds
        Bounds bounds = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TunnelManager>()
                        .tunnelSegmentNormal.transform.GetChild(2).GetComponent<MeshRenderer>().bounds;
        // Get bounds' extents
        boundsExtents = bounds.extents;
        // Find player object
        player = GameObject.FindGameObjectWithTag("Player");
        // Initialize x & y delta speed and last speed
        xDeltaSpeed = 0.0f;
        yDeltaSpeed = 0.0f;
        xSpeedLast = 0.0f;
        ySpeedLast = 0.0f;
        // Start the particle systems for the jets
        transform.FindChild("Right Jet").GetComponent<ParticleSystem>().Play();
        transform.FindChild("Left Jet").GetComponent<ParticleSystem>().Play();
    }
	
	// Update is called once per frame
	void Update () {
        // Check if locked
        if (!Input.GetKey(KeyCode.Q)) {
            // Get ship position
            Vector3 shipPosition = transform.position;
            // Get ray from cursor at camera
            Ray aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Find where cursor is pointing ship to go
            Vector2 destination = (Vector2)aimRay.GetPoint(Vector3.Distance(shipPosition, aimRay.origin)) + new Vector2(0, cursorOffset);
            // Initialize a vector that translates the ship
            Vector3 translateVector = new Vector3();

            // Find how far ship can move on each axis (uses the equation of an elipse)
            float xExtents = Mathf.Sqrt((boundsExtents.x * boundsExtents.x) * (1 - ((shipPosition.y * shipPosition.y) / (boundsExtents.y * boundsExtents.y))));
            float yExtents = Mathf.Sqrt((boundsExtents.y * boundsExtents.y) * (1 - ((shipPosition.x * shipPosition.x) / (boundsExtents.x * boundsExtents.x))));

            // Determine x and y speeds
            float xSpeed = nimbilityFactor * (destination.x - shipPosition.x);
            float ySpeed = nimbilityFactor * (destination.y - shipPosition.y);

            // Check if x speed exceeds max speed
            if (xSpeed < 0)
                xSpeed = Mathf.Max(xSpeed, -maxSpeed);
            else if (xSpeed > 0)
                xSpeed = Mathf.Min(xSpeed, maxSpeed);

            // Check if y speed exceeds max speed
            if (ySpeed < 0)
                ySpeed = Mathf.Max(ySpeed, -maxSpeed);
            else if (ySpeed > 0)
                ySpeed = Mathf.Min(ySpeed, maxSpeed);

            // Calculate delta speeds
            xDeltaSpeed = xSpeedLast - xSpeed;
            yDeltaSpeed = ySpeedLast - ySpeed;

            // Set last speeds
            xSpeedLast = xSpeed;
            ySpeedLast = ySpeed;

            // Check if ship is at bounds
            if ((shipPosition.x > destination.x) && (shipPosition.x > -xExtents))
                // Set the x of the translate vector
                translateVector.x = xSpeed * Time.deltaTime;
            else if ((shipPosition.x < destination.x) && (shipPosition.x < xExtents))
                // Set the x of the translate vector
                translateVector.x = xSpeed * Time.deltaTime;

            if ((shipPosition.y > destination.y) && (shipPosition.y > -yExtents))
                // Set the z of the translate vector (ship is rotated on x axis, so z is up)
                translateVector.z = ySpeed * Time.deltaTime;
            else if ((shipPosition.y < destination.y) && (shipPosition.y < yExtents))
                // Set the z of the translate vector
                translateVector.z = ySpeed * Time.deltaTime;

            // Translate ship
            transform.Translate(translateVector);
        }
    }

    // When collision happens
    void OnTriggerEnter(Collider collider) {
        // Check if collision was with obstacle
        if (collider.tag == "Obstacle") {
            // Play sound effect
            transform.GetComponent<AudioSource>().Play();
            // Check if player has HP
            if (player.GetComponent<PlayerCharacter>().fullCondition > 0) {
                // Damage player
                player.GetComponent<PlayerCharacter>().doDamage(10.0f);
                // Play explosion animation
                GameObject.FindGameObjectWithTag("Ship").GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
