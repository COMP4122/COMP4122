using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Item {

	private Player player;

	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	public override void GotPickedUp() {

		player.SetShootModeToBow ();

		Destroy(this.gameObject);

	}
}
