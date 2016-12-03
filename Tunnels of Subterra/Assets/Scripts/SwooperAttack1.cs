using UnityEngine;
using System.Collections;

public class SwooperAttack1 : MonoBehaviour {

    public float attackDistance = 35.0f;
    public float searchSpeed = 50.0f;

    private float hoverSpeed;

    private enum states { searching, attacking }
    private int state = (int) states.searching;

    private Animation swooperAnimation;

    private GameObject ship;

	// Use this for initialization
	void Start () {
        swooperAnimation = transform.GetComponent<Animation>();
        hoverSpeed = -GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().levelSpeed;
        ship = GameObject.FindGameObjectWithTag("Ship");
    }
	
	// Update is called once per frame
	void Update () {
	    switch (state) {
            case (int) states.searching:
                swooperAnimation["Fly"].speed = 2.0f;
                swooperAnimation.Play("Fly");
                if ((transform.position.z - ship.transform.position.z) > attackDistance)
                    transform.Translate(0, 0, searchSpeed * Time.deltaTime);
                else
                    state = (int) states.attacking;
                break;
            case (int) states.attacking:
                transform.Translate(0, 0, hoverSpeed * Time.deltaTime);
                swooperAnimation["Attack"].speed = 2.0f;
                swooperAnimation.Play("Attack");
                break;
        }
	}
}

