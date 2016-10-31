using System.Collections;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour {

    public GameObject[] collectables;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float y;

    public float minSpawnInterval;
    public float maxSpawnInterval;

    void Start() {
        StartCoroutine(SpawningCoroutine());
    }

    IEnumerator SpawningCoroutine() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
            int i = Random.value > 0.5f ? 0 : 1;
            Instantiate(collectables[i], new Vector3(Random.Range(minX, maxX), y, Random.Range(minZ, maxZ)), new Quaternion());
        }
    }
}
