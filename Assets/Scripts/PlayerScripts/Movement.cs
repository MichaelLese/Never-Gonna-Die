using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Dash dash;

    [SerializeField] private float moveSpeed = 5f;
    private float newMoveSpeed;
    [SerializeField] private float smoothTime = 0.1f;
    private float newSmoothTime;

    private Vector2 moveDirection;
    private Vector2 currentVelocity = Vector2.zero;
    private float moveAngle;
    Vector3 dir;

    // TODO: Make agility actually do something
    private int agilityLevel;

    public void InitMovement(int agility) {
        agilityLevel = agility;

        switch (agility) {
            case 0:
                newMoveSpeed = moveSpeed;
                newSmoothTime = smoothTime;
                break;
            case 1:
                newMoveSpeed = (float)(1.25) * moveSpeed;
                newSmoothTime = (float)(2) * smoothTime;
                break;

            case 2:
                newMoveSpeed = (float)(1.60) * moveSpeed;
                newSmoothTime = (float)(5) * smoothTime;
                break;

            case 3:
                newMoveSpeed = (float)(2) * moveSpeed;
                newSmoothTime = (float)(9) * smoothTime;
                break;
            default:
                newMoveSpeed = moveSpeed;
                newSmoothTime = smoothTime;
                break;
        }
    }

    public void UpdateMovement(Rigidbody2D rb) {
        if (dash.GetIsDashing() == false) {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector2(moveX, moveY).normalized;
            moveAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
            // Lerp current velocity towards the target direction
            currentVelocity = Vector2.Lerp(currentVelocity, moveDirection * newMoveSpeed, newSmoothTime * Time.deltaTime);
            // Apply the lerped velocity to the Rigidbody
            rb.velocity = currentVelocity;

            dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else {
            currentVelocity = Vector2.Lerp(currentVelocity, moveDirection * dash.GetDashSpeed(), newSmoothTime * Time.deltaTime);
            rb.velocity = currentVelocity;
        }
    }

    public void StartDash() {
        dash.DoDash(moveAngle);
    }

    public Vector3 GetMousePosition() {
        return dir;
    }
}
