using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

public class PlayerController : MonoBehaviour {
    public Rigidbody2D rb;
    public Movement movement;
    public Weapons weapon;
    public PlayerHealth health;
    public Upgrades upgrades;
    public ScreenManager screenManager;
    public PlayerUI playerUI;
    public LevelManager levelManager;

    // Start is called before the first frame update
    void Start() {
        UpdateValues();
        screenManager.InitScreenManager();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            weapon.Fire(movement.GetMousePosition());
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            movement.StartDash();
        }
        movement.UpdateMovement(rb);

        if (health.GetIsDead()) {
            screenManager.TriggerGameOver();    // Go to the game over screen!
            Debug.Log("End Game");
        }
    }

    public void UpdateValues() {
        //Initialize Upgrades
        upgrades.InitUpgrades();
        //Initialize Health
        health.InitHealth(upgrades.GetShield());
        //Initialize movement (for agility)
        movement.InitMovement(upgrades.GetAgility());
        //Initialize weapon damage
        weapon.InitWeapons(upgrades.GetCrit());
        //Initializes the PlayerUI
        playerUI.InitPlayerUI(health.GetMaxHearts(), health.GetCurrentShield(), levelManager.GetRoundTime());
    }

    public void UpdateHearts()
    {
        playerUI.UpdateHeartsDisplay(health.GetCurrentHearts(), health.GetMaxHearts(), health.GetCurrentShield(), health.GetMaxShield());
    }   public void SetHealthShields() {
        health.SetShields(upgrades.GetShield());
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Asteroid")) {
            Debug.Log("Player Hit!");
            health.TakeDamage(1); // Change to this: other.gameObject.GetComponent<AsteroidController>().damage
            UpdateHearts();
        }
    }
}
