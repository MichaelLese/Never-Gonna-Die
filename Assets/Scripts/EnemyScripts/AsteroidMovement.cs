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
    private bool isInZone = false;
    private float arenaRadius;

    public void UpdateMovement(Rigidbody2D rb) {
        // Lerp current velocity towards the target direction
        currentVelocity = Vector2.Lerp(currentVelocity, moveDirection * moveSpeed, smoothTime * Time.deltaTime);
        // Apply the lerped velocity to the Rigidbody
        rb.velocity = currentVelocity;
    }

    public void FindFirstTarget() {
        //TODO: Make it so it doesn't always go straight to the middle (slightly to the side randomly)
        if (Vector2.Distance(transform.position, Vector2.zero) > arenaRadius) {
            isInZone = false;
        }
        else {
            isInZone = true;
        }

        moveDirection = (-transform.position).normalized;
        Invoke("FindNewTarget", retargetTimer);
    }

    public void FindNewTarget() {
        if (Vector2.Distance(transform.position, Vector2.zero) > arenaRadius) {
            isInZone = false;
        }
        else {
            isInZone = true;
        }

        if (isInZone) {
            moveDirection = Random.insideUnitCircle.normalized;
            Invoke("FindNewTarget", retargetTimer);
        }
        else {
            moveDirection = (-transform.position).normalized;
            Invoke("FindNewTarget", retargetTimer);
            Debug.Log("Asteroid is OUT");
        }
    }

    public void StartAsteroid(float radius) {
        arenaRadius = radius;
        FindFirstTarget();
    }
}
