using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float health = 100f;

    private Watcher watcher;
	private PlayerShoot playerShoot;
	private CraftManager craftManager;

	void Start() {
        watcher = GameObject.FindGameObjectWithTag("SceneController").GetComponent<Watcher>();
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
        if (damage > health)
            damage = health;

		health -= damage;
        watcher.SetHealth(health);
	}
		
	public bool IsReadyForMakingBow() {
		return craftManager.IsReadyForMakingBow ();
	}

	public void MakeBow() {
		craftManager.MakeBow ();
	}
}
