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

    // TODO: Make agility actually do something
    private int agilityLevel;

    public void initMovement(int agility) {
        agilityLevel = agility;

        switch (agility) {
            case 0:
                //Do Nothing
                break;
            case 1:
                moveSpeed = (float)(1.25) * moveSpeed;
                smoothTime = (float)(2) * smoothTime;
                break;

            case 2:
                moveSpeed = (float)(1.60) * moveSpeed;
                smoothTime = (float)(5) * smoothTime;
                break;

            case 3:
                moveSpeed = (float)(2) * moveSpeed;
                smoothTime = (float)(9) * smoothTime;
                break;
            default:
                //Do nothing
                break;
        }
    }

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
    }

    public Vector3 getMousePosition() {
        return dir;
    }
}
