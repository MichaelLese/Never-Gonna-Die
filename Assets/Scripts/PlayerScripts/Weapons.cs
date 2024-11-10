using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float normalShotCooldown = 0.25f;
    public float burstShotCooldown = 1.75f;
    public float shotgunShotCooldown = 1.0f;

    private float lastShot;

    public float normalFireForce = 20f;
    public float burstFireForce = 20f;
    public float shotgunFireForce = 20f;


    private int critLevel;



    public void Start() {
        lastShot = Time.time - normalShotCooldown;
    }

    public void InitWeapons(int crit) {
        critLevel = crit;
    }

    public void Fire(Vector3 mousePosition) {
        FireShotgun(mousePosition); //For now no if statement
    }

    public void FireNormal(Vector3 mousePosition) {
        if ((Time.time - lastShot) >= normalShotCooldown) {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null) {
                bulletScript.damage = CalculateDamage(critLevel);
                // TODO: maybe change so it also changes color when it is a crit
            }
            Vector2 direction = (mousePosition).normalized;
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * normalFireForce, ForceMode2D.Impulse);

            lastShot = Time.time;
        }
    }

    public void FireBurst(Vector3 mousePosition) {
        if ((Time.time - lastShot) >= burstShotCooldown) {
            StartCoroutine(FireBurstCoroutine(mousePosition));
            lastShot = Time.time;
        }
    }

    private IEnumerator FireBurstCoroutine(Vector3 mousePosition) {
        int burstCount = 4; // Number of bullets in the burst
        for (int i = 0; i < burstCount; i++) {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null) {
                bulletScript.damage = CalculateDamage(critLevel);
                // TODO: change color if it’s a crit
            }

            Vector2 direction = (mousePosition - firePoint.position).normalized;
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * burstFireForce, ForceMode2D.Impulse);

            // Wait 0.05 seconds before firing the next bullet
            yield return new WaitForSeconds(0.05f);
        }
    }
    public void FireShotgun(Vector3 mousePosition) {
        if ((Time.time - lastShot) >= shotgunShotCooldown) {
            float spreadAngle = 8f;

            // Fire 5 bullets (for example)
            for (int i = 0; i <= 4; i++) {
                // Create a spread
                float angleOffset = Random.Range(-spreadAngle, spreadAngle);
                // Create the bullet
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null) {
                    bulletScript.damage = CalculateDamage(critLevel);
                }

                // Calculate the direction of the bullet with the spread angle
                Vector2 direction = (mousePosition - firePoint.position).normalized;

                // Rotate the direction by the spread angle
                direction = RotateVector2(direction, angleOffset);

                // Apply force to the bullet
                bullet.GetComponent<Rigidbody2D>().AddForce(direction * shotgunFireForce, ForceMode2D.Impulse);
            }

            lastShot = Time.time;
        }
    }

    // Helper function to rotate a vector2 by a given angle (in degrees)
    private Vector2 RotateVector2(Vector2 v, float angle) {
        float radianAngle = angle * Mathf.Deg2Rad; // Convert angle to radians
        float cosAngle = Mathf.Cos(radianAngle);
        float sinAngle = Mathf.Sin(radianAngle);

        // Rotate the vector
        float x = v.x * cosAngle - v.y * sinAngle;
        float y = v.x * sinAngle + v.y * cosAngle;

        return new Vector2(x, y);
    }

    private int CalculateDamage(int crit) {
        // Random number will check if its a crit or not
        // 5 levels of crit
        int randomNumber = Random.Range(0, 11); // 0 to 10 inclusive
        if (randomNumber == 10) {
            return 3;
        }
        else if (randomNumber >= ((2 * crit)-1)) {
            return 1;
        } else {
            return 2;
        }
    }
}
