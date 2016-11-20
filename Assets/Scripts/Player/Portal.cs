using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

	public string destinationScene;

    private Watcher watcher;

    void Start() {
        watcher = GameObject.FindGameObjectWithTag("SceneController").GetComponent<Watcher>();
    }

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player") {
            watcher.ChangeScene(destinationScene);
		}
	}
}
