using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private GameObject emptyHeartPrefab;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private GameObject emptyShieldPrefab;

    [SerializeField] private Transform heartsContainer;

    public void InitHearts(int maxHearts, int maxShields) {
        UpdateHeartsDisplay(maxHearts, maxHearts, maxShields, maxShields);
    }

    public void UpdateHeartsDisplay(int currentHearts, int maxHearts, int currentShield, int maxShields) {
        // Clear any existing heart images in the container
        foreach (Transform child in heartsContainer) {
            Destroy(child.gameObject);
        }

        // Instantiate new full heart images based on current health
        for (int i = 0; i < currentHearts; i++) {
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
}
