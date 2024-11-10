using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int levelCount = 5;
    private int currentLevel = 1;
    [SerializeField] private float[] spawnSpeed;

    public int GetCurrentLevel() {
        return currentLevel;
    }

}
