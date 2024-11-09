using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothTime = 0.1f;

    private Vector2 moveDirection;
    private Vector2 currentVelocity = Vector2.zero;

    Vector3 dir;

    public void updateMovement(Rigidbody2D rb) {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        // Lerp current velocity towards the target direction
        currentVelocity = Vector2.Lerp(currentVelocity, moveDirection * moveSpeed, smoothTime * Time.deltaTime);
        // Apply the lerped velocity to the Rigidbody
        rb.velocity = currentVelocity;

        dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public Vector3 getMousePosition() {
        return dir;
    }
}
