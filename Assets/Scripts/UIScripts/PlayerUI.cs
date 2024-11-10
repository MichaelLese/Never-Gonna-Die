using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject emptyHeartPrefab;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject emptyShieldPrefab;

    [SerializeField] private Transform heartsContainer;
    [SerializeField] private TextMeshProUGUI timerText;

    public void InitPlayerUI(int maxHearts, int maxShields, float countdownTime) {
        UpdateHeartsDisplay(maxHearts, maxHearts, maxShields, maxShields);
        StartCountdown(countdownTime);
    }

    public void UpdateHeartsDisplay(int currentHearts, int maxHearts, int currentShield, int maxShields) {
        // Clear any existing heart images in the container
        foreach (Transform child in heartsContainer) {
            Destroy(child.gameObject);
        }

        // Instantiate new full heart images based on current health
        for (int i = 0; i < Mathf.Min(currentHearts, maxHearts); i++) {
            Instantiate(heartPrefab, heartsContainer);
        }
        // Instantiate new empty heart images based on current health
        for (int i = 0; i < maxHearts - currentHearts; i++) {
            Instantiate(emptyHeartPrefab, heartsContainer);
        }
        // Instantiate new fuill shield images based on current shield
        for (int i = 0; i < currentShield; i++) {
            Instantiate(shieldPrefab, heartsContainer);
        }
        // Instantiate new empty shield images based on current shield
        for (int i = 0; i < maxShields - currentShield; i++) {
            Instantiate(emptyShieldPrefab, heartsContainer);
        }
    }

    public void StartCountdown(float countdownTime) {
        StartCoroutine(CountdownCoroutine(countdownTime));
    }

    private IEnumerator CountdownCoroutine(float countdownTime) {
        while (countdownTime > 0) {
            // Update the timer text in MM:SS format
            timerText.text = $"{Mathf.FloorToInt(countdownTime / 60):00}:{Mathf.FloorToInt(countdownTime % 60):00}";
            yield return new WaitForSeconds(1f);
            countdownTime--;
            }
        timerText.text = "00:00";

        ScreenManager sm = GetComponent<ScreenManager>();
        sm.TriggerUpgradeScreen();
    }
    }
