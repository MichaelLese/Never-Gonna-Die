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

    // Start is called before the first frame update
    public void initUpgrades()
    {
        agility = 0;
        crit = 0;
        shield = 0;
    }

    public int getAgility() {
        return agility; 
    }    
    public void increaseAgility() {
        if (agility < maxAgility) {
            agility++;
        }
    }

    public int getCrit() {
        return crit;
    }
    public void increaseCrit() {
        if (crit < maxCrit) {
            crit++;
        }
    }

    public int getShield() {
        return shield;
    }
    public void increaseShield() {
        if (shield < maxShield) {
            shield++;
        }
    }
}
