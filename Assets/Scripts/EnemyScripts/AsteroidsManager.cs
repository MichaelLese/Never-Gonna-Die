using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour {
    [SerializeField] private float arenaRadius = 10f;
    public GameObject astroidPrefab;


    // Start is called before the first frame update
    void Start() {

    }

    private void manageAstroids() {
        Invoke("manageAstroids", 5f);
        createAstroid();
    }

    private void createAstroid() {
        // Generate a random angle in radians
        float angle = Random.Range(0f, Mathf.PI * 2);
        // Calculate the position on the perimeter of the arena
        Vector3 spawnPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * arenaRadius;
        // Calculate rotation to look toward the center (0,0)
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -spawnPosition);
        // Instantiate the asteroid at the perimeter, facing the center
        GameObject asteroid = Instantiate(astroidPrefab, spawnPosition, rotation);

        AsteroidController astroidScript = asteroid.GetComponent<AsteroidController>();
        if (astroidScript != null) {
            astroidScript.setArenaRadius(arenaRadius);
        }
    }
}
