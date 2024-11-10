using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shotCooldown = 10f;
    private float lastShot;
    public float fireForce = 20f;

    private int critLevel;

    public void Start() {
        lastShot = Time.time - shotCooldown;
    }

    public void InitWeapons(int crit) {
        critLevel = crit;
    }

    public void Fire(Vector3 mousePosition) {
        if ((Time.time - lastShot) >= shotCooldown) {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null) {
                bulletScript.damage = CalculateDamage(critLevel);
                // TODO: maybe change so it also changes color when it is a crit
            }
            Vector2 direction = (mousePosition).normalized;
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * fireForce, ForceMode2D.Impulse);

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
        else if (randomNumber < ((2 * crit)-1)) {
            return 1;
        } else {
            return 2;
        }
    }
}
