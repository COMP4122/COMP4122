using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Item {

	private CraftManager craftManager;

	void Start() {
		craftManager = GameObject.FindGameObjectWithTag ("Player").GetComponent<CraftManager> ();
	}

	public override void GotPickedUp() {

		craftManager.PickRock ();

		Destroy(this.gameObject);

	}
}
