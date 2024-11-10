using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHealth : MonoBehaviour {
    [SerializeField] private int numHearts = 5;
    private int currentHearts;
    private bool isDead;

    private SpriteRenderer sr;

    public void initHealth() {
        currentHearts = numHearts;
        sr = GetComponent<SpriteRenderer>();
    }

    public float getCurrentHearts() {
        return currentHearts;
    }

    public bool getIsDead() {
        return isDead;
    }

    public void flashRed() {
        sr.color = new Color(1f, 0.81f, 0.81f, 1f);
        Invoke("returnColor", 0.1f);
    }
    public void returnColor() {
        sr.color = Color.white;
    }

    public void takeDamage(int damageInHearts) {
        currentHearts -= damageInHearts;
        isDead |= currentHearts <= 0;
    }
}

//TODO: Give play short invincibility