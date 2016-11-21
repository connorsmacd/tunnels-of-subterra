using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {

	public float condition;
    public float maxCondition = 100.0f;
	public float fullCondition = 100.0f;
	public float armourModifier = 1;
	public int score;
	public int experience;

	public void doDamage (float damage) {
		fullCondition -= (damage * armourModifier);
	}

    public void heal(float hitpoints)
    {
        if (fullCondition + hitpoints < maxCondition)
        {
            fullCondition += hitpoints;
        }
        else
        {
            fullCondition = maxCondition;
        }
    }

    public void modifyScore (int addToScore) {
		score += addToScore;
	}

	void startLevel () {
		condition = fullCondition;
	}

    public float getHealth()
    {
        return fullCondition;
    }

    public int getScore()
    {
        return score;
    }
}
