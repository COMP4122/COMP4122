using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorController : MonoBehaviour {

	public GameObject[] survivors;

	private int survivorCount;
	private GameDataController dc;

	void Start() {
		dc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameDataController> ();
        UpdateSurvivorCount();
	}
		
	public void UpdateSurvivorCount() {
		survivorCount = dc.getData().survivorCount;
        
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
        if (survivorCount <= 0)
            dc.GameOver();
    }


}
