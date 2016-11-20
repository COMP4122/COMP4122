using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour {

	public bool hasWood = false;
	public bool hasRock = false;

	public void PickWood() {
		hasWood = true;
	}

	public void PickRock() {
		hasRock = true;
	}		

	public bool IsReadyForMakingBow() {
		return hasRock && hasWood;
	}

	public void MakeBow() {
		hasWood = false;
		hasRock = false;
	}
}
