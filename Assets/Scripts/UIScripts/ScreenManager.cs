using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Weapons;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject startScreenCanvas;
    [SerializeField] private GameObject endScreenCanvas;
    [SerializeField] private GameObject upgradeScreenCanvas;
    [SerializeField] private GameObject instructionScreenCanvas;
    [SerializeField] private GameObject winScreenCanvas;
    [SerializeField] private GameObject pauseScreenCanvas;

    [SerializeField] private GameObject lockedBar;
    [SerializeField] private GameObject unlockedBar;
    [SerializeField] private Transform critContainer;
    [SerializeField] private Transform agilityContainer;
    [SerializeField] private Transform shieldContainer;
    [SerializeField] private Transform normalWeaponContainer;
    [SerializeField] private Transform burstWeaponContainer;
    [SerializeField] private Transform shotgunWeaponContainer;

    [SerializeField] private Button sacrificeButton;
    [SerializeField] private Button critButton;
    [SerializeField] private Button agilityButton;
    [SerializeField] private Button shieldButton;
    [SerializeField] private Button normalWeaponButton;
    [SerializeField] private Button burstWeaponButton;
    [SerializeField] private Button shotgunWeaponButton;
    [SerializeField] private Button equipNormalWeaponButton;
    [SerializeField] private Button equipBurstWeaponButton;
    [SerializeField] private Button equipShotgunWeaponButton;
    [SerializeField] private Image equipedGunImage;
    [SerializeField] private TextMeshProUGUI waveText;

    [SerializeField] private TextMeshProUGUI scrapTotal;

    private bool isPaused = false;

    [SerializeField] private Upgrades upgrades;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Weapons weapons;


    public void InitScreenManager() {
        startScreenCanvas.SetActive(true); 
        endScreenCanvas.SetActive(false);
        upgradeScreenCanvas.SetActive(false);
        instructionScreenCanvas.SetActive(false);
        winScreenCanvas.SetActive(false);
        pauseScreenCanvas.SetActive(false);
        Time.timeScale = 0.0f;
    }
    public void StartGame() {
        startScreenCanvas.SetActive(false); // Hide the start screen
        Time.timeScale = 1f;                // Resume game speed
    }

    public void TriggerPauseScreen() {
        pauseScreenCanvas.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
    }

    public void TriggerLeavePauseScreen() {
        pauseScreenCanvas.SetActive(false);
        
        if (AreAllScreensInactive()) {
            Time.timeScale = 1.0f;
        }
        isPaused = false;
    }
    public bool GetIsPaused() {
        return isPaused;
    }
    private bool AreAllScreensInactive() {
        return !startScreenCanvas.activeSelf && !endScreenCanvas.activeSelf && !upgradeScreenCanvas.activeSelf && !instructionScreenCanvas.activeSelf && !winScreenCanvas.activeSelf;
    }

    public void TriggerWinScreen() {
        startScreenCanvas.SetActive(false);
        endScreenCanvas.SetActive(false);
        upgradeScreenCanvas.SetActive(false);
        instructionScreenCanvas.SetActive(false);
        winScreenCanvas.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void TriggerLeaveWinScreen() {
        winScreenCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }


    public void TriggerGameOver() {
        endScreenCanvas.SetActive(true);    // Show the end screen
        Time.timeScale = 0f;                // Pause the game
    }

    public void TriggerInstructions() {
        instructionScreenCanvas.SetActive(true);
    }
    public void TriggerLeaveInstructions() {
        instructionScreenCanvas.SetActive(false);
    }

    public void TriggerUpgradeScreen() {
        playerUI.UpdateHeartsDisplay(health.GetCurrentHearts(), health.GetMaxHearts(), health.GetCurrentShield(), health.GetMaxShield());
        upgradeScreenCanvas.SetActive(true);// Show the upgrade screen
        waveText.text = "START WAVE " + (levelManager.GetCurrentLevel() + 1);
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

        if (upgrades.GetScrap() < 5 || weapons.GetNormalIsUpgraded()) {
            normalWeaponButton.enabled = false;
        } else { normalWeaponButton.enabled = true; }
        if (upgrades.GetScrap() < 5 || weapons.GetBurstIsUpgraded()) {
            burstWeaponButton.enabled = false;
        } else { burstWeaponButton.enabled = true; }
        if (upgrades.GetScrap() < 5 || weapons.GetShotgunIsUpgraded()) {
            shotgunWeaponButton.enabled = false;
        } else { shotgunWeaponButton.enabled = true; }

        if (weapons.GetOwnsBurst()) {
            equipBurstWeaponButton.enabled = true;
        }
        else {
            equipBurstWeaponButton.enabled = false;
        }
        if (weapons.GetOwnsShotgun()) {
            equipShotgunWeaponButton.enabled = true;
        }
        else {
            equipShotgunWeaponButton.enabled = false;
        }

        if (health.GetMaxHearts() <= 1) {
            sacrificeButton.enabled = false;
        }
        else {
            sacrificeButton.enabled = true;
        }

        switch (weapons.currentGun) {
            case GunType.Normal:
                equipedGunImage.sprite = Resources.Load<Sprite>("Misc/NormalGunImage");
                break;
            case GunType.Burst:
                equipedGunImage.sprite = Resources.Load<Sprite>("Misc/BurstGunImage");
                break;
            case GunType.Shotgun:
                equipedGunImage.sprite = Resources.Load<Sprite>("Misc/ShotgunGunImage");
                break;
        }

        UpdateScrapTotal();
    }

    public void UpdateUpgradeDisplays() {
        //For the ship upgrades
        foreach (Transform child in critContainer) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in agilityContainer) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in shieldContainer) {
            Destroy(child.gameObject);
        }

        //For the weapon upgrades
        foreach (Transform child in normalWeaponContainer) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in burstWeaponContainer) {
            Destroy(child.gameObject);
        }
        foreach (Transform child in shotgunWeaponContainer) {
            Destroy(child.gameObject);
        }

        //For Ship Upgrades
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

        //For Weapon upgrades/purchases\
        //For Normal Weapon
        Instantiate(unlockedBar, normalWeaponContainer);
        if (weapons.GetNormalIsUpgraded()) {
            Instantiate(unlockedBar, normalWeaponContainer);
        }
        else {
            Instantiate(lockedBar, normalWeaponContainer);
        }

        //For Burst Weapon
        if (weapons.GetOwnsBurst()) {
            Instantiate(unlockedBar, burstWeaponContainer);
        }
        else {
            Instantiate(lockedBar, burstWeaponContainer);
        }
        if (weapons.GetBurstIsUpgraded()) {
            Instantiate(unlockedBar, burstWeaponContainer);
        }
        else {
            Instantiate(lockedBar, burstWeaponContainer);
        }

        //For shotgun
        if (weapons.GetOwnsShotgun()) {
            Instantiate(unlockedBar, shotgunWeaponContainer);
        }
        else {
            Instantiate(lockedBar, shotgunWeaponContainer);
        }
        if (weapons.GetShotgunIsUpgraded()) {
            Instantiate(unlockedBar, shotgunWeaponContainer);
        }
        else {
            Instantiate(lockedBar, shotgunWeaponContainer);
        }
    }
}
