using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHealth : MonoBehaviour {
    [SerializeField] private int numHearts = 5;
    private int currentHearts;
    private bool isDead;

    private SpriteRenderer sr;

    public void InitHealth() {
        currentHearts = numHearts;
        sr = GetComponent<SpriteRenderer>();
    }

    public float GetCurrentHearts() {
        return currentHearts;
    }

    public bool GetIsDead() {
        return isDead;
    }

    public void FlashRed() {
        sr.color = new Color(1f, 0.81f, 0.81f, 1f);
        Invoke("ReturnColor", 0.1f);
    }
    public void ReturnColor() {
        sr.color = Color.white;
    }

    public void TakeDamage(int damageInHearts) {
        currentHearts -= damageInHearts;
        isDead |= currentHearts <= 0;
    }
}

//TODO: Give play short invincibility