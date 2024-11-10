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

    public void InitHealth(int shields) {
        currentHearts = numHearts;
        maxShields = shields;
    }

    public float GetCurrentHearts() {
        return currentHearts;
    }
    public float GetCurrentShield() {
        return currentShields;
    }
    public bool GetIsDead() {
        return isDead;
    }

    public void TakeDamage(int damageInHearts) {
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