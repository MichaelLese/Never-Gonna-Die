using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int numHearts = 10;
    private int currentHearts;
    private bool isDead;

    public void initHealth() {
        currentHearts = numHearts;
    }

    public float getCurrentHearts() {
        return currentHearts;
    }

    public bool getIsDead() {
        return isDead;
    }

    public void takeDamage(int damageInHearts) {
        currentHearts -= damageInHearts;
        isDead |= currentHearts <= 0;
    }
}

//TODO: Give play short invincibility