using UnityEngine;
using System.Collections;

public class FriendSpawner : MonoBehaviour {

    public GameObject friend;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float y;

    public float minSpawnInterval;
    public float maxSpawnInterval;

    void Start()
    {
        StartCoroutine(SpawningCoroutine());
    }

    IEnumerator SpawningCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
            Instantiate(friend, new Vector3(Random.Range(minX, maxX), y, Random.Range(minZ, maxZ)), new Quaternion());
        }
    }
}
