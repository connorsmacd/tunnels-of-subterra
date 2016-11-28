using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {

	public float condition;
    public float maxCondition = 100.0f;
	public float fullCondition = 100.0f;
    public float maxShield = 200f;
    public float shield = 0f;
    public float armourModifier = 1;
	public int score;
	public int experience;
    public string playerName;

	public void doDamage (float damage) {

        float damageAmount = (damage * armourModifier);

        if (shield > 0)
        {
            shield -= damageAmount;
            if(shield < 0)
            {
                fullCondition += shield;
                shield = 0;
            }
        }
        else
        {
            fullCondition -= (damage * armourModifier);
        }
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

    public void addShield(float amount)
    {
        if (shield + amount < maxShield)
        {
            shield += amount;
        }
        else
        {
            shield = maxShield;
        }
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
    public void setName(string n)
    {
        playerName = n;
    }
    public string getName()
    {
        return playerName;
    }

    public float getShield() {
        return shield;
    }
}
