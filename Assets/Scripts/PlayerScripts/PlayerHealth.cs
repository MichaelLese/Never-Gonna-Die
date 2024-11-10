using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int numHearts = 10;
    private int currentHearts;
    private bool isDead;

    private int maxShields;
    public int currentShields;

    public void InitHealth(int shields) {
        currentHearts = numHearts;
        maxShields = shields;
        currentShields = shields;
    }

    public int GetCurrentHearts() {
        return currentHearts;
    }
    public int GetMaxHearts() {
        return numHearts;
    }
    public int GetCurrentShield() {
        return currentShields;
    }
    public int GetMaxShield() {
        return maxShields;
    }
    public bool GetIsDead() {
        return isDead;
    }

    public void TakeDamage(int damageInHearts) {
        if (currentShields > 0) {
            currentShields -=damageInHearts;
        }
        else {
            currentHearts -= damageInHearts;
            isDead |= currentHearts <= 0;
        }
    }
}

//TODO: Give play short invincibility