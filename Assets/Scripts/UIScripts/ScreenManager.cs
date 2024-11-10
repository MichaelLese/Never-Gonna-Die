using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreenCanvas;
    [SerializeField] private GameObject endScreenCanvas;
    [SerializeField] private GameObject upgradeScreenCanvas;

    public void InitScreenManager() {
        startScreenCanvas.SetActive(true); 
        endScreenCanvas.SetActive(false);
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
}
