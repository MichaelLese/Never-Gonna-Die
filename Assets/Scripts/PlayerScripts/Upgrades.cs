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
    public void IncreaseAgility() {
        if (agility < maxAgility) {
            agility++;
        }
    }

    public int GetCrit() {
        return crit;
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
    public void IncreaseShield() {
        if (shield < maxShield) {
            shield++;
        }
    }
}
