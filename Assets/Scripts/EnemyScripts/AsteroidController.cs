using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public Rigidbody2D rb;
    public AsteroidMovement movement;
    public AsteroidHealth health;

    // TODO: Put this in the even larger enemies controller script eventually
    [SerializeField] private float arenaRadius = 10f; 

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
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Asteroid")) {
            Debug.Log("Player Hit!");
            health.takeDamage(1); // Change to this: other.gameObject.GetComponent<AsteroidController>().damage
        }
    }
}
