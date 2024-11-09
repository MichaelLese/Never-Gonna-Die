using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float timeToLiveBullets = 4f;
    public float damage = 10f;

    public void Start() {
        Destroy(gameObject, timeToLiveBullets);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.gameObject);
        if (!collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
}