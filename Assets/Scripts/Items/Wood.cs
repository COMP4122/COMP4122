using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Item {

	private CraftManager craftManager;

	void Start() {
		craftManager = GameObject.FindGameObjectWithTag ("Player").GetComponent<CraftManager> ();
	}

	public override void GotPickedUp() {

		craftManager.PickWood ();

		Destroy(this.gameObject);

	}
}
