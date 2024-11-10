using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private Upgrades upgrades;


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
        upgradeScreenCanvas.SetActive(true);// Show the upgrade screen
        UpdateUpgradeDisplays();
        Time.timeScale = 0f;                // Pause the game
    }

    public void TriggerUpgradeScreenLeave() {
        upgradeScreenCanvas.SetActive(false);// Hide the upgrade screen
        Time.timeScale = 1f;                 // Resume the game speed
    }

    public void RetryGame() {
        Time.timeScale = 1f;                // Resume game speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene (Probably need to do something else here)
    }

    public void QuitGame() {
        Application.Quit();                 // Quit the application
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
