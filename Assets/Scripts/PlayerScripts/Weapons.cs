using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shotCooldown = 10f;
    private float lastShot;
    public float fireForce = 20f;
    public void Start() {
        lastShot = Time.time - shotCooldown;
    }
    public void Fire(Vector3 mousePosition) {
        if ((Time.time - lastShot) >= shotCooldown) {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Vector2 direction = (mousePosition).normalized;
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * fireForce, ForceMode2D.Impulse);

            lastShot = Time.time;
        }
    }
}
