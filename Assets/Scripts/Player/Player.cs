using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float health = 100f;

	private PlayerShoot playerShoot;
	private CraftManager craftManager;

	void Start() {
		playerShoot = GetComponent<PlayerShoot> ();
		craftManager = GetComponent<CraftManager> ();
	}

	public void SetShootModeToBow() {
		playerShoot.SwitchToBow ();
	}

	public void SetShootModeToRock() {
		playerShoot.SwitchToRock ();
	}

	public void TakeDamage(float damage) {
		health -= damage;

		if (health <= 0) {
			Die ();
		}
	}

	public void Die() {
		
	}
		
	public bool IsReadyForMakingBow() {
		return craftManager.IsReadyForMakingBow ();
	}

	public void MakeBow() {
		craftManager.MakeBow ();
	}
}
