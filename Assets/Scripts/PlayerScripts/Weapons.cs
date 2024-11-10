using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float normalShotCooldown = 0.25f;
    public float burstShotCooldown = 1.0f;
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
        FireNormal(mousePosition); //For now no if statement
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
            for (int i = 0; i < 4; i++) {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null) {
                    bulletScript.damage = CalculateDamage(critLevel);
                    // TODO: maybe change so it also changes color when it is a crit
                }
                Vector2 direction = (mousePosition).normalized;
                bullet.GetComponent<Rigidbody2D>().AddForce(direction * burstFireForce, ForceMode2D.Impulse);
            }
            lastShot = Time.time;

        }
    }
    public void FireShotgun(Vector3 mousePosition) {
        if ((Time.time - lastShot) >= shotgunShotCooldown) {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null) {
                bulletScript.damage = CalculateDamage(critLevel);
                // TODO: maybe change so it also changes color when it is a crit
            }
            Vector2 direction = (mousePosition).normalized;
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * shotgunFireForce, ForceMode2D.Impulse);

            lastShot = Time.time;
        }
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
