using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private int maxAgility;
    [SerializeField] private int agility;

    [SerializeField] private int maxCrit;
    [SerializeField] private int crit;

    [SerializeField] private int maxShield;
    [SerializeField] private int shield;

    public void InitUpgrades()
    {
        //agility = 3;
        //crit = 5;
        //shield = 3;
    }

    public int GetAgility() {
        return agility; 
    }
    public int GetMaxAgility() {
        return maxAgility;
    }
    public void IncreaseAgility() {
        if (agility < maxAgility) {
            agility++;
        }
    }

    public int GetCrit() {
        return crit;
    }
    public int GetMaxCrit() {
        return maxCrit;
    }
    public void IncreaseCrit() {
        if (crit < maxCrit) {
            crit++;
        }
    }

    public int GetShield() {
        Debug.Log("shield: " + shield);
        return shield;
    }
    public int GetMaxShield() {
        Debug.Log("shield: " + shield);
        return maxShield;
    }
    public void IncreaseShield() {
        if (shield < maxShield) {
            shield++;
        }
    }
}
