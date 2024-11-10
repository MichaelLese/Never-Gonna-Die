using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int numHearts = 10;
    private int currentHearts;
    private bool isDead;

    private int maxShields;
    private int currentShields;

    public void initHealth(int shields) {
        currentHearts = numHearts;
        maxShields = shields;
    }

    public float getCurrentHearts() {
        return currentHearts;
    }
    public float getCurrentShield() {
        return currentShields;
    }
    public bool getIsDead() {
        return isDead;
    }

    public void takeDamage(int damageInHearts) {
        if (currentShields > 0) {
            currentShields--;
        }
        else {
            currentHearts -= damageInHearts;
            isDead |= currentHearts <= 0;
        }
    }
}

//TODO: Give play short invincibility