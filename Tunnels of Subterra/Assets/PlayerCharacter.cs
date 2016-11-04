using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {

	public float condition;
	public float fullCondition = 100.0f;
	public float armourModifier;
	public int score;
	public int experience;

	void doDamage (float damage) {
		condition -= (damage * armourModifier);
	}

	void modifyScore (int addToScore) {
		score += addToScore;
	}

	void startLevel () {
		condition = fullCondition;
	}
}
