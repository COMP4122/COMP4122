using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Watcher : MonoBehaviour {
    public float autoSaveDuration;

    private GameDataController dc;
    private GameData data;
    private GameObject player;

    void Start() {
        dc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameDataController>();
        player = GameObject.FindGameObjectWithTag("Player");
        data = dc.getData();
        if (data.sceneName != "Snow") {
            data.sceneName = "Snow";
        }
        else {
            Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
            Quaternion rotation = new Quaternion(data.rotation[1], data.rotation[2], data.rotation[3], data.rotation[0]);
            player.transform.position = position;
            player.transform.rotation = rotation;
        }
        StartCoroutine(AutoSave());
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            Save();            
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            dc.Load(1);
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            if (SceneManager.GetActiveScene().name == "Snow")
                SceneManager.LoadScene("Camp");
            if (SceneManager.GetActiveScene().name == "Camp")
                SceneManager.LoadScene("Snow");
        }
    }

    IEnumerator AutoSave() {
        while (true) {
            yield return new WaitForSeconds(autoSaveDuration);
            Save();
        }
    }

    private void Save() {
        data.position[0] = player.transform.position.x;
        data.position[1] = player.transform.position.y;
        data.position[2] = player.transform.position.z;
        data.rotation[0] = player.transform.rotation.w;
        data.rotation[1] = player.transform.rotation.x;
        data.rotation[2] = player.transform.rotation.y;
        data.rotation[3] = player.transform.rotation.z;
        dc.Save(data, 1);
    }
}
