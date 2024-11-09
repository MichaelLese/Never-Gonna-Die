using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Dash dash;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float smoothTime = 0.1f;

    private Vector2 moveDirection;
    private Vector2 currentVelocity = Vector2.zero;
    private float moveAngle;
    Vector3 dir;

    public void updateMovement(Rigidbody2D rb) {
        if (dash.getIsDashing() == false) {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector2(moveX, moveY).normalized;
            moveAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
            // Lerp current velocity towards the target direction
            currentVelocity = Vector2.Lerp(currentVelocity, moveDirection * moveSpeed, smoothTime * Time.deltaTime);
            // Apply the lerped velocity to the Rigidbody
            rb.velocity = currentVelocity;

            dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else {
            currentVelocity = Vector2.Lerp(currentVelocity, moveDirection * dash.getDashSpeed(), smoothTime * Time.deltaTime);
            rb.velocity = currentVelocity;
        }
    }

    public void startDash() {
        dash.dash(moveAngle);
        Debug.Log("Dash Started in Movement");
    }

    public Vector3 getMousePosition() {
        return dir;
    }
}
