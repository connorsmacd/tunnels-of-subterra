/*
    File: PlayerCharacter.cs
    Authors: Connor S. MacDonald (B00632423)
             Cole DeMan
             Mike McPhee

    This file is used to hold all of the player's stats
*/

using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {
    // Condition of the ship
	public float condition;
    // Maximum allowable condition of th ship
    public float maxCondition = 100.0f;
    // Full condition of the ship
	public float fullCondition = 100.0f;
    // Maximum allowable shield value
    public float maxShield = 200f;
    // Shield value of ship
    public float shield = 0f;
    // Armour modifier - is multiplied by damage taken to reduce it
    public float armourModifier = 1;
    // Current score of the player in the level
	public int score;
    // Amount of currency player has
    public int currency;
    // Experience of the player
	public int experience;
    // Name of player
    public string playerName;

    // Reduces the condition of the player
	public void doDamage (float damage) {
        // Determine amount of damage
        float damageAmount = (damage * armourModifier);
        // Check if there is a shield active
        if (shield > 0)
        {
            // Take damage off shield
            shield -= damageAmount;
            // Check if shield is finished
            if(shield < 0)
            {
                // Add remaining damage not absorbed by the shield
                fullCondition += shield;
                // Set shield to zero
                shield = 0;
            }
        }
        else
        {
            // Subtract from condition
            fullCondition -= (damage * armourModifier);
        }
	}

    // Adds to the ships condition
    public void heal(float hitpoints)
    {
        // Check if the heal will exceed maximum contion
        if (fullCondition + hitpoints < maxCondition)
        {
            // Add to condition
            fullCondition += hitpoints;
        }
        else
        {
            // Set condition to max condition
            fullCondition = maxCondition;
        }
    }

    // Modifies the player's score
    public void modifyScore (int addToScore) {
        // Add to the score
		score += addToScore;
	}

    // Adds a shield to the ship
    public void addShield(float amount)
    {
        // Check if shield addition will exceed max shield
        if (shield + amount < maxShield)
        {
            // Add to shield
            shield += amount;
        }
        else
        {
            // Set shield to max shield
            shield = maxShield;
        }
    }

    // Sets the condition to full condition
    void startLevel () {
		condition = fullCondition;
	}

    // Returns the current condition of the ship
    public float getHealth()
    {
        return fullCondition;
    }

    // Returns score of the ship
    public int getScore()
    {
        return score;
    }

    // Sets the player name
    public void setName(string n)
    {
        playerName = n;
    }

    // Returns the player name
    public string getName()
    {
        return playerName;
    }

    // Gets the current shield value
    public float getShield() {
        return shield;
    }
}
