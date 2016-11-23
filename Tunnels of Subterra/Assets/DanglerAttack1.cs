using UnityEngine;
using System.Collections;

public class DanglerAttack1 : MonoBehaviour {

    public float pounceProximity = 4.0f;
    public float jumpSpeed = 4.0f;
    public float gravity = 8.0f;
    private Vector3 translationVector = Vector3.zero;
    private int state = 0;
    private Animation danglerAnimation;
    private bool pouncing = false;
    private bool hanging = false;

	// Use this for initialization
	void Start () {
        danglerAnimation = gameObject.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 danglerPosition = transform.position;
        Vector3 shipPosition = GameObject.FindGameObjectWithTag("Ship").transform.position;

        if (!hanging) {
            if (Mathf.Abs(danglerPosition.z - shipPosition.z) > pounceProximity) {
                danglerAnimation.Play("Idle");
            } else {
                pouncing = true;
                translationVector.y += jumpSpeed;
            }

            if (pouncing) {
                danglerAnimation["pounce"].speed = 2f;
                danglerAnimation.Play("pounce");
                translationVector.y -= gravity * Time.deltaTime;
                transform.Translate(translationVector * Time.deltaTime);
            }
        } else {
            danglerAnimation.Play("attack");
        }
	}

    public void setHanging () {
        hanging = true;
        pouncing = false;
        transform.parent = GameObject.FindGameObjectWithTag("Ship").transform;
    }
}
