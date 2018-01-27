using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject thingToSpawn;
    public int numToSpawn;
    [Range(0, 10)]
    public float timeBetweenSpawns;
    [Range(0, 10)]
    public float timeBeforeFirstSpawn;

    private List<GameObject> spawnedThings = new List<GameObject>();
    private Transform[] spawnPositions;

    void Start() {
        StartCoroutine(Spawn());
        spawnPositions = transform.GetDirectChildren();
        if (spawnPositions.Length == 0) {
            spawnPositions = new[] {transform};
        }
    }

    private IEnumerator Spawn() {
        yield return new WaitForSeconds(timeBeforeFirstSpawn);
        while (true) {
            for (int i = spawnedThings.Count - 1; i >= 0; i--) {
                if (spawnedThings[i] == null) {
                    spawnedThings.RemoveAt(i);
                }
            }

            if (spawnedThings.Count < numToSpawn) {
                var spawnPos = spawnPositions[Random.Range(0, spawnPositions.Length)];
                spawnedThings.Add(Instantiate(thingToSpawn, spawnPos.position, Quaternion.identity));
            }

            yield return new WaitForSeconds(timeBetweenSpawns); 
        }
    }
}