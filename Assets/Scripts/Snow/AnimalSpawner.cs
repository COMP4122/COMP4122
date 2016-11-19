using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour {
    public GameObject[] spawnList;
    public int maxAnimalNumber;
    public float spawnDuration;

    private GameObject[] spawnPoints;
    private Transform player;
    private Camera mainCamera;
    private float nextSpawn = 0.0f;
    private bool stopSpawn = false;

    void Start() {
        spawnPoints = GameObject.FindGameObjectsWithTag("Navigation");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mainCamera = Camera.main;
    }

    void Update() {
        if (!stopSpawn && Time.time > nextSpawn && GameObject.FindGameObjectsWithTag("Enemy").Length < maxAnimalNumber) {
            int index = Mathf.FloorToInt(Random.Range(0.0f, spawnPoints.Length));
            Vector3 position = mainCamera.WorldToViewportPoint(spawnPoints[index].transform.position);
            if ((position.x < 0.0f || position.x > 1.0f) && (position.y < 0.0f || position.y > 1.0f) && Vector3.Distance(spawnPoints[index].transform.position, player.position) > 10.0f) {
                int item = Mathf.FloorToInt(Random.Range(0.0f, spawnList.Length));
                Instantiate(spawnList[item], spawnPoints[index].transform.position, Quaternion.identity);
                nextSpawn = Time.time + spawnDuration + Random.Range(-5.0f, 5.0f);
            }
        }
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == maxAnimalNumber)
            stopSpawn = true;
    }

}
