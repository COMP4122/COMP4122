using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeArea : MonoBehaviour {

	public GameObject bow;

	void Start() {
		bow.SetActive (false);
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player") {
			Player player = collider.gameObject.GetComponent<Player> ();
			if (player.IsReadyForMakingBow ()) {
				player.MakeBow ();
				bow.SetActive (true);
			}
		}
	}

}
