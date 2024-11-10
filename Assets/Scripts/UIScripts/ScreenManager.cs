using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreenCanvas;
    [SerializeField] private GameObject endScreenCanvas;
    [SerializeField] private GameObject upgradeScreenCanvas;

    [SerializeField] private GameObject lockedBar;
    [SerializeField] private GameObject unlockedBar;
    [SerializeField] private Transform critContainer;
    [SerializeField] private Transform agilityContainer;
    [SerializeField] private Transform shieldContainer;

    [SerializeField] private Button sacrificeButton;
    [SerializeField] private Button critButton;
    [SerializeField] private Button agilityButton;
    [SerializeField] private Button shieldButton;

    [SerializeField] private TextMeshProUGUI scrapTotal;

    [SerializeField] private Upgrades upgrades;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerUI playerUI;


    public void InitScreenManager() {
        startScreenCanvas.SetActive(true); 
        endScreenCanvas.SetActive(false);
        upgradeScreenCanvas.SetActive(false);
        Time.timeScale = 0.0f;
    }
    public void StartGame() {
        startScreenCanvas.SetActive(false); // Hide the start screen
        Time.timeScale = 1f;                // Resume game speed
    }

    public void TriggerGameOver() {
        endScreenCanvas.SetActive(true);    // Show the end screen
        Time.timeScale = 0f;                // Pause the game
    }

    public void TriggerUpgradeScreen() {
        playerUI.UpdateHeartsDisplay(health.GetCurrentHearts(), health.GetMaxHearts(), health.GetCurrentShield(), health.GetMaxShield());
        upgradeScreenCanvas.SetActive(true);// Show the upgrade screen
        UpdateUpgradeDisplays();
        UpdateButtons();
        Time.timeScale = 0f;                // Pause the game
    }

    public void TriggerUpgradeScreenLeave() {
        startScreenCanvas.SetActive(false);
        endScreenCanvas.SetActive(false);
        upgradeScreenCanvas.SetActive(false);
        Time.timeScale = 1f;                 // Resume the game speed
    }

    public void RetryGame() {
        Time.timeScale = 1f;                // Resume game speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene (Probably need to do something else here)
    }

    public void QuitGame() {
        Application.Quit();                 // Quit the application
    }

    public void UpdateScrapTotal() {
        scrapTotal.text = "" + upgrades.GetScrap() + " Scrap";
    }

    public void UpdateButtons() {
        if (upgrades.GetCrit() >= upgrades.GetMaxCrit() || upgrades.GetScrap() < 1) {
            critButton.enabled = false;
        }
        else { critButton.enabled = true; }
        if (upgrades.GetAgility() >= upgrades.GetMaxAgility() || upgrades.GetScrap() < 2) {
            agilityButton.enabled = false;
        }
        else { agilityButton.enabled = true; }
        if (upgrades.GetShield() >= upgrades.GetMaxShield() || upgrades.GetScrap() < 2) {
            shieldButton.enabled = false;
        }
        else { shieldButton.enabled = true; }

        if (health.GetMaxHearts() <= 1) {
            sacrificeButton.enabled = false;
        }
        else {
            sacrificeButton.enabled = true;
        }

        UpdateScrapTotal();
    }

    public void UpdateUpgradeDisplays() {
        foreach (Transform child in critContainer) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in agilityContainer) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in shieldContainer) {
            Destroy(child.gameObject);
        }

        // Instantiate new full heart images based on current health
        for (int i = 0; i < upgrades.GetShield(); i++) {
            Instantiate(unlockedBar, shieldContainer);
        }
        for (int i = 0; i < upgrades.GetMaxShield() - upgrades.GetShield(); i++) {
            Instantiate(lockedBar, shieldContainer);
        }

        // Instantiate new empty heart images based on current health
        for (int i = 0; i < upgrades.GetAgility(); i++) {
            Instantiate(unlockedBar, agilityContainer);
        }
        for (int i = 0; i < upgrades.GetMaxAgility() - upgrades.GetAgility(); i++) {
            Instantiate(lockedBar, agilityContainer);
        }

        // Instantiate new fuill shield images based on current shield
        for (int i = 0; i < upgrades.GetCrit(); i++) {
            Instantiate(unlockedBar, critContainer);
        }
        for (int i = 0; i < upgrades.GetMaxCrit() - upgrades.GetCrit(); i++) {
            Instantiate(lockedBar, critContainer);
        }
    }
}
