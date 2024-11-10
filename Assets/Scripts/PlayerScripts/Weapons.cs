using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {
    public enum GunType {
        Normal,
        Burst,
        Shotgun
    }
    public GunType currentGun = GunType.Normal;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private bool normalIsUpgraded = false;
    private bool burstIsUpgraded = false;
    private bool shotgunIsUpgraded = false;

    private bool ownsBurst = false;
    private bool ownsShotgun = false;

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
        switch (currentGun) {
            case GunType.Normal:
                FireNormal(mousePosition);
                break;
            case GunType.Burst:
                FireBurst(mousePosition);
                break;
            case GunType.Shotgun:
                FireShotgun(mousePosition);
                break;
        }
    }

    public void FireNormal(Vector3 mousePosition) {
        if ((Time.time - lastShot) >= normalShotCooldown) {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null) {

                if (normalIsUpgraded) {
                    bulletScript.damage = CalculateDamage(critLevel) + 1; // Upgrade adds one damage
                    // TODO: maybe change so it also changes color when it is a crit
                }
                else {
                    bulletScript.damage = CalculateDamage(critLevel);
                }
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
        int burstCount = 4;
        if (burstIsUpgraded) {
            burstCount = 6; // Upgrade increases # of bullets
        }
        
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

            int burstCount = 4;
            if (shotgunIsUpgraded) {
                burstCount = 7; //Upgrade increases the # of bullets
            }

            // Fire 5 bullets (for example)
            for (int i = 0; i <= burstCount; i++) {
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

    public bool GetNormalIsUpgraded() {
        return normalIsUpgraded;
    }
    public void SetNormalIsUpgraded() {
        normalIsUpgraded = true;
    }
    public bool GetBurstIsUpgraded() {
        return burstIsUpgraded;
    }
    public void SetBurstIsUpgraded() {
        burstIsUpgraded = true;
    }
    public bool GetShotgunIsUpgraded() {
        return shotgunIsUpgraded;
    }
    public void SetShotgunIsUpgraded() {
        shotgunIsUpgraded = true;
    }

    public bool GetOwnsBurst() {
        return ownsBurst;
    }
    public void SetOwnsBurst() {
        ownsBurst = true;
    }
    public bool GetOwnsShotgun() {
        return ownsShotgun;
    }
    public void SetOwnsShotgun() {
        ownsShotgun = true;
    }

    //Functions for the buttons to use
    public void pressNormalButton() {
        normalIsUpgraded = true;
    }
    public void pressBurstButton() {
        if (!ownsBurst) {
            ownsBurst |= true;
        }
        else {
            burstIsUpgraded = true;
        }
    }
    public void pressShotgunButton() {
        if (!ownsShotgun) {
            ownsShotgun |= true;
        }
        else {
            shotgunIsUpgraded = true;
        }
    }

    public void equipNormal() {
        currentGun = GunType.Normal;
    }
    public void equipBurst() {
        currentGun = GunType.Burst;
    }public void equipShotgun() {
        currentGun = GunType.Shotgun;
    }
}
