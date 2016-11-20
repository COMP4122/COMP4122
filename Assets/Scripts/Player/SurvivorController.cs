using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorController : MonoBehaviour {

	public GameObject[] survivors;

	private int survivorCount = 3;
	private Watcher watcher;

	void Awake() {
		watcher = GameObject.FindGameObjectWithTag ("SceneController").GetComponent<Watcher> ();
	}

	public void UpdateSurvivorCount() {
		survivorCount = watcher.GetSurvivorCount ();

		if (survivorCount > 3) {
			Debug.Log ("There should not be more than 3 survivors");
		} else {
			
			foreach (GameObject survivor in survivors) {
				survivor.SetActive (false);
			}

			for (int i = 0; i < survivorCount; i++) {
				survivors [i].SetActive (true);
			}
		}
			
	}


}
