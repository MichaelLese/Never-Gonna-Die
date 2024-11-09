using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Movement movement;
    public Weapons weapon;
    public PlayerHealth health;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Health
        health.initHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            weapon.Fire(movement.getMousePosition());
        } 
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            movement.startDash();
        }
        movement.updateMovement(rb);

        if (health.getIsDead()) {
            //End the game
            Debug.Log("End Game");
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Asteroid")) {
            Debug.Log("Player Hit!");
            health.takeDamage(1); // Change to this: other.gameObject.GetComponent<AsteroidController>().damage
        }
    }
}
