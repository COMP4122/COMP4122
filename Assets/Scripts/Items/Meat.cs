using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : Item {

	public int noOfFood;
    // TODO

    // public GameDataController gameDataController;

    void Start() {
        // gameDataController = GameObject.FindGameObjectsWithTag("GameDataController");
    }

	public override void GotPickedUp() {

        // TODO: gameDataController method here

        Destroy(this.gameObject);

    }
}
