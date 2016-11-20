using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

	public string destinationScene;

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player") {
			SceneManager.LoadScene (destinationScene);
		}
	}
}
