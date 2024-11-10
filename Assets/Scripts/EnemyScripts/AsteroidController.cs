using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public Rigidbody2D rb;
    public AsteroidMovement movement;
    public AsteroidHealth health;


    // Gets this from the astroidsManager script
    [SerializeField] private float arenaRadius;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private bool isSplitter = false;
    [SerializeField] private GameObject splitAsteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
        health.InitHealth();
        movement.StartAsteroid(arenaRadius);
    }

    // Update is called once per frame
    void Update()
    {
        movement.UpdateMovement(rb);

        if (health.GetIsDead()) {
            //Destroy asteroid and play an effect (Explosion)
            Explode();
            if (isSplitter) {
                Instantiate(splitAsteroidPrefab, transform.position, transform.rotation);
                Instantiate(splitAsteroidPrefab, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    private void Explode() {
        // Instantiate explosion at asteroid's position and rotation
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    public void SetArenaRadius(float radius) {
        arenaRadius = radius;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("PlayerBullet")) {
            Debug.Log("Asteroid Hit! It took damage: " + health.GetCurrentHearts() + " From a bullet that did: " + other.gameObject.GetComponent<Bullet>().damage);
            health.TakeDamage(other.gameObject.GetComponent<Bullet>().damage);
            health.FlashRed();
        }
    }
}
