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
    private float lastDash = 0;
    private bool isDashing = false;

    //public float getDashSpeed() { return currentDashSpeed; }

    public void DoDash(float moveAngle) {
        if ((Time.time - lastDash) >= dashCooldown) {
            currentDashSpeed = dashSpeed;
            lastDash = Time.time;
            isDashing = true;
            isRecovering = true;

            Invoke("EndDash", dashActive);
            Invoke("SetRecoveringFalse", dashRecovery);
        }
    }

    public float GetDashSpeed() {
        return dashSpeed;
    }

    public void EndDash() {
        currentDashSpeed = 0;
        isDashing = false;
    }

    public bool GetIsDashing() {
        return isDashing;
    }

    public float GetDashRecovery() {
        return dashRecovery;
    }

    public bool GetRecovering() {
        return isRecovering;
    }

    public void SetRecoveringFalse() {
        isRecovering = false;
    }
}
