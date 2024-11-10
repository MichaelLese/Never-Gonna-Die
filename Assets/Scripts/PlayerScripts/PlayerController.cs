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
    public Upgrades upgrades;
    public ScreenManager screenManager;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize Health
        health.InitHealth(upgrades.GetShield());
        //Initialize Upgrades
        upgrades.InitUpgrades();
        //Initialize movement (for agility)
        movement.InitMovement(upgrades.GetAgility());
        //Initialize weapon damage
        weapon.InitWeapons(upgrades.GetCrit());
        //Initialize the screen manager
        screenManager.InitScreenManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            weapon.Fire(movement.GetMousePosition());
        } 
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            movement.StartDash();
        }
        movement.UpdateMovement(rb);

        if (health.GetIsDead()) {
            //End the game
            screenManager.TriggerGameOver();
            Debug.Log("End Game");
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Asteroid")) {
            Debug.Log("Player Hit!");
            health.TakeDamage(1); // Change to this: other.gameObject.GetComponent<AsteroidController>().damage
        }
    }
}
