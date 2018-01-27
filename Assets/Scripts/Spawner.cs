using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject thingToSpawn;
    public int numToSpawn;
    [Range(0, 10)]
    public float timeBetweenSpawns;
    public float timeBeforeFirstSpawn;

    private List<GameObject> spawnedThings = new List<GameObject>();

    void Start() {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() {
        while (true) {
            
        }
    }
}