using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dash : MonoBehaviour {
    public float dashSpeed = 15f;
    private float currentDashSpeed;
    public float dashActive = 0.2f;
    public float dashCooldown = 2f;
    public float dashRecovery = 1.25f;
    public bool isRecovering = false;
    private float lastDash;
    private bool isDashing = false;

    public float getDashSpeed() { return currentDashSpeed; }

    public void dash(float moveAngle) {
        if ((Time.time - lastDash) >= dashCooldown) {
            currentDashSpeed = dashSpeed;
            lastDash = Time.time;
            isDashing = true;
            isRecovering = true;

            Invoke("endDash", dashActive);
            Invoke("setRecoveringFalse", dashRecovery);
        }
    }

    public void endDash() {
        currentDashSpeed = 0;
        isDashing = false;
    }

    public bool getIsDashing() {
        return isDashing;
    }

    public float getDashRecovery() {
        return dashRecovery;
    }

    public bool getRecovering() {
        return isRecovering;
    }

    public void setRecoveringFalse() {
        isRecovering = false;
    }
}
