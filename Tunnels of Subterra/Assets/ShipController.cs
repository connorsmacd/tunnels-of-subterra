using UnityEngine;
using System.Collections;

/*
 * Script: ShipController.cs
 * Author: Connor MacDonald (B00632423) & Cole W. DeMan (B00602412)
 * 
 * This script controls the ship.
 */

public class ShipController : MonoBehaviour {

    public float nimbilityFactor = 0.10f;
    public float maxSpeed = 10f;
    public float cursorOffset = -0.5f;

    public float xDeltaSpeed { get; set; }
    public float yDeltaSpeed { get; set; }
    public float xSpeedLast { get; set; }
    public float ySpeedLast { get; set; }

    private GameObject player;
    private Vector2 boundsExtents;

    // Use this for initialization
    void Start () {
        Bounds bounds = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<TunnelManager>()
                        .tunnelSegmentNormal.transform.GetChild(2).GetComponent<MeshRenderer>().bounds;
        boundsExtents = bounds.extents;
        player = GameObject.FindGameObjectWithTag("Player");
        xDeltaSpeed = 0.0f;
        yDeltaSpeed = 0.0f;
        xSpeedLast = 0.0f;
        ySpeedLast = 0.0f;
        transform.FindChild("Right Jet").GetComponent<ParticleSystem>().Play();
        transform.FindChild("Left Jet").GetComponent<ParticleSystem>().Play();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 shipPosition = transform.position;
        Ray aimRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector2 destination = (Vector2) aimRay.GetPoint(Vector3.Distance(shipPosition, aimRay.origin)) + new Vector2(0, cursorOffset);
        Vector3 translateVector = new Vector3();

        float xExtents = Mathf.Sqrt((boundsExtents.x * boundsExtents.x) * (1 - ((shipPosition.y * shipPosition.y) / (boundsExtents.y * boundsExtents.y))));
        float yExtents = Mathf.Sqrt((boundsExtents.y * boundsExtents.y) * (1 - ((shipPosition.x * shipPosition.x) / (boundsExtents.x * boundsExtents.x))));

        float xSpeed = nimbilityFactor * (destination.x - shipPosition.x);
        float ySpeed = nimbilityFactor * (destination.y - shipPosition.y);

        if (xSpeed < 0)
            xSpeed = Mathf.Max(xSpeed, -maxSpeed);
        else if (xSpeed > 0)
            xSpeed = Mathf.Min(xSpeed, maxSpeed);

        if (ySpeed < 0)
            ySpeed = Mathf.Max(ySpeed, -maxSpeed);
        else if (ySpeed > 0)
            ySpeed = Mathf.Min(ySpeed, maxSpeed);

        xDeltaSpeed = xSpeedLast - xSpeed;
        yDeltaSpeed = ySpeedLast - ySpeed;

        xSpeedLast = xSpeed;
        ySpeedLast = ySpeed; 

        if ((shipPosition.x > destination.x) && (shipPosition.x > -xExtents))
            translateVector.x = xSpeed * Time.deltaTime;
        else if ((shipPosition.x < destination.x) && (shipPosition.x < xExtents))
            translateVector.x = xSpeed * Time.deltaTime;

        if ((shipPosition.y > destination.y) && (shipPosition.y > -yExtents))
            translateVector.z = ySpeed * Time.deltaTime;
        else if ((shipPosition.y < destination.y) && (shipPosition.y < yExtents))
            translateVector.z = ySpeed * Time.deltaTime;

        transform.Translate(translateVector);
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.tag == "Obstacle") {
            print("Player collided with obstacle");
            if (player.GetComponent<PlayerCharacter>().fullCondition > 0) {
                player.GetComponent<PlayerCharacter>().doDamage(10.0f);
                GameObject.FindGameObjectWithTag("Ship").GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
