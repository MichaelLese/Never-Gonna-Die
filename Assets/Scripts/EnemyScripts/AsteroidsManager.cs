using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour {
    [SerializeField] private float arenaRadius = 50f;
    [SerializeField] private float waitTime = 5f;
    public GameObject bigAsteroidPrefab;
    public GameObject splitterAsteroidPrefab;
    private GameObject spawnedAsteroid;


    // Start is called before the first frame update
    void Start() {
        manageAstroids();
    }

    private void manageAstroids() {
        createAstroid();
        Invoke("manageAstroids", waitTime);
    }

    private void createAstroid() {
        if (Random.Range(0, 2) == 0) {
            spawnedAsteroid = bigAsteroidPrefab;
        }
        else {
            spawnedAsteroid = splitterAsteroidPrefab;
        }

        // Generate a random angle in radians
        float angle = Random.Range(0f, Mathf.PI * 2);
        // Calculate the position on the perimeter of the arena
        Vector2 spawnPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized * (arenaRadius);
        Vector3 spawnPos = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        // Calculate rotation to look toward the center (0,0)
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -spawnPos);
        // Instantiate the asteroid at the perimeter, facing the center
        GameObject asteroid = Instantiate(spawnedAsteroid, spawnPosition, rotation);

        AsteroidController astroidScript = spawnedAsteroid.GetComponent<AsteroidController>();
        if (astroidScript != null) {
            astroidScript.setArenaRadius(arenaRadius);
        }
    }
}
