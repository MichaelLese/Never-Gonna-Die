using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AsteroidMovement : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private float retargetTimer = 10f;

    private Vector2 moveDirection;
    private Vector2 currentVelocity = Vector2.zero;
    private bool isInZone = true;
    private float arenaRadius;

    public void updateMovement(Rigidbody2D rb) {
        // Lerp current velocity towards the target direction
        currentVelocity = Vector2.Lerp(currentVelocity, moveDirection * moveSpeed, smoothTime * Time.deltaTime);
        // Apply the lerped velocity to the Rigidbody
        rb.velocity = currentVelocity;
    }

    public void findNewTarget() {
        if (Vector2.Distance(transform.position, Vector2.zero) > arenaRadius) {
            isInZone = false;
        }
        else {
            isInZone = true;
        }

        if (isInZone) {
            moveDirection = Random.insideUnitCircle.normalized;
            Invoke("findNewTarget", retargetTimer);
        }
        else {
            moveDirection = (-transform.position).normalized;
            Invoke("findNewTarget", retargetTimer);
            Debug.Log("Asteroid is OUT");
        }
    }

    public void startAsteroid(float radius) {
        arenaRadius = radius;
        findNewTarget();
    }
}
