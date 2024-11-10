using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int levelCount = 6; //Endless counts as a level kind of
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private float[] spawnSpeed;
    [SerializeField] private float roundTime = 5f;

    public int GetCurrentLevel() {
        return currentLevel;
    }
    public void IncreaseCurrentLevel() {
        currentLevel++; 
    }

    public float GetSpawnSpeed(int level) {
        return spawnSpeed[level];
    }

    public float GetRoundTime() {
        return roundTime; 
    }
}
