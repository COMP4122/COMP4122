using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : MonoBehaviour {

	public int noOfFood;

    // public GameDataController gameDataController;

    void Start() {
        // gameDataController = GameObject.FindGameObjectsWithTag("GameDataController");
    }

    public void GotPickedUp() {

        // TODO: gameDataController method here

        Destroy(this.gameObject);

    }
}
