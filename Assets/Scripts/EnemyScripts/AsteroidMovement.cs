using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothTime = 0.1f;

    private Vector2 moveDirection;
    private Vector2 currentVelocity = Vector2.zero;
    private float moveAngle;
    Vector3 dir;

    public void updateMovement(Rigidbody2D rb) {
        moveDirection = new Vector2(0,0).normalized;
        // Lerp current velocity towards the target direction
        currentVelocity = Vector2.Lerp(currentVelocity, moveDirection * moveSpeed, smoothTime * Time.deltaTime);
        // Apply the lerped velocity to the Rigidbody
        rb.velocity = currentVelocity;
    }
}
