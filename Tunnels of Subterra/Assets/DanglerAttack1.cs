using UnityEngine;
using System.Collections;

public class DanglerAttack1 : MonoBehaviour {

    public float pounceProximity = 4.0f;
    public float gravity = 8.0f;
    public float maxDeltaSpeed = 8.0f;

    private Vector3 translationVector = Vector3.zero;
    private Animation danglerAnimation;

    private GameObject ship;
    private ShipController shipController;

    private enum states { idle, pouncing, hanging, flung };
    private int state = 0;

	// Use this for initialization
	void Start () {
        danglerAnimation = gameObject.GetComponent<Animation>();
        ship = GameObject.FindGameObjectWithTag("Ship");
        shipController = ship.transform.GetComponent<ShipController>();
    }
	
	// Update is called once per frame
	void Update () {
        switch (state) {
            case (int) states.idle:
                Bounds shipBounds = ship.GetComponent<MeshRenderer>().bounds;
                Vector3 shipFront = shipBounds.center + new Vector3(0, 0, shipBounds.extents.z);
                if (transform.position.z - shipFront.z > pounceProximity) {
                    danglerAnimation.Play("Idle");
                } else {
                    state = (int) states.pouncing;
                    float shipSpeed = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().levelSpeed;
                    float shipTravelTime = pounceProximity / shipSpeed;
                    Vector3 colliderPosition = transform.GetChild(0).transform.position;
                    translationVector.z = -(shipFront.x - colliderPosition.x) / shipTravelTime;
                    translationVector.y = ((shipFront.y - colliderPosition.y) 
                                            - (0.5f * -gravity * shipTravelTime * shipTravelTime)) / shipTravelTime;
                }
                break;
            case (int) states.pouncing:
                danglerAnimation["pounce"].speed = 2f;
                danglerAnimation.Play("pounce");
                translationVector.y -= gravity * Time.deltaTime;
                transform.Translate(translationVector * Time.deltaTime);
                if (transform.position.z < ship.transform.position.z)
                    state = (int) states.flung;
                break;
            case (int) states.hanging:
                danglerAnimation.Play("attack");
                if ((Mathf.Abs(shipController.xDeltaSpeed) > maxDeltaSpeed) ||
                    (Mathf.Abs(shipController.yDeltaSpeed) > maxDeltaSpeed)) {
                    beginFlinging();
                }
                break;
            case (int) states.flung:
                danglerAnimation["pounce"].speed = 2f;
                danglerAnimation.Play("pounce");
                translationVector.y -= gravity * Time.deltaTime;
                transform.Translate(translationVector * Time.deltaTime);
                break;
        }
    }

    public void beginHanging (Transform hangTransform) {
        if (state != (int)states.hanging) {
            state = (int)states.hanging;
            Vector3 xOriginal = new Vector3(transform.position.x,
                                            hangTransform.position.y,
                                            hangTransform.position.z);
            transform.parent = hangTransform;
            transform.rotation = hangTransform.rotation;
            transform.position = xOriginal;
            float attackTime = danglerAnimation["attack"].length;
            transform.GetComponent<Collider>().enabled = false;
            InvokeRepeating("damageShip", attackTime, attackTime);
        } 
    }

    public void beginFlinging () {
        transform.parent = null;
        transform.rotation = Quaternion.Euler(0, -90, 0);
        translationVector.y = shipController.ySpeedLast;
        translationVector.z = shipController.xSpeedLast;
        CancelInvoke();
        state = (int) states.flung;
    }

    void damageShip () {
        ship.transform.parent.GetComponent<PlayerCharacter>().doDamage(3);
    }
}

