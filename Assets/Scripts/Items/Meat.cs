using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : Item {

	public int noOfFood;
    private Watcher watcher;

    // public GameDataController gameDataController;

    void Start() {
        watcher = GameObject.FindGameObjectWithTag("SceneController").GetComponent<Watcher>();
    }

	public override void GotPickedUp() {

        // TODO: gameDataController method here
        watcher.addMeat(noOfFood);
        Destroy(this.gameObject);

    }
}
