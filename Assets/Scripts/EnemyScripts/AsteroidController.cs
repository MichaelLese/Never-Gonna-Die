using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public Rigidbody2D rb;
    public AsteroidMovement movement;
    public AsteroidHealth health;

    // TODO: Put this in the even larger enemies controller script eventually
    [SerializeField] private float arenaRadius = 10f;

    [SerializeField] private GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        health.initHealth();
        movement.startAsteroid(arenaRadius);
    }

    // Update is called once per frame
    void Update()
    {
        movement.updateMovement(rb);

        if (health.getIsDead()) {
            //Destroy asteroid and play an effect (Explosion)
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode() {
        // Instantiate explosion at asteroid's position and rotation
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("PlayerBullet")) {
            Debug.Log("Asteroid Hit!");
            health.takeDamage(1); // Change to this: other.gameObject.GetComponent<AsteroidController>().damage
        }
    }
}
