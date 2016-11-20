using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Item {

	public int noOfFood;
	private Watcher watcher;

	void Start() {
		watcher = GameObject.FindGameObjectWithTag("SceneController").GetComponent<Watcher>();
	}

	public override void GotPickedUp() {

		watcher.addMeat(noOfFood);
		Destroy(this.gameObject);

	}
}
