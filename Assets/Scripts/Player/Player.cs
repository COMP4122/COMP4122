using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public AudioClip[] audioClips;
    public float health = 100f;

    private AudioSource audioSource;
    private Watcher watcher;
	private PlayerShoot playerShoot;
	private CraftManager craftManager;

	void Start() {
        audioSource = GetComponent<AudioSource>();
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
        PlayHurtSound();
        health -= damage;
        watcher.SetHealth(health);
	}
		
	public bool IsReadyForMakingBow() {
		return craftManager.IsReadyForMakingBow ();
	}

	public void MakeBow() {
		craftManager.MakeBow ();
	}

    void PlayHurtSound()
    {
        int audioClipsLength = audioClips.Length;
        audioSource.clip = audioClips[Mathf.FloorToInt(Random.Range(0, 1.99999f))];
        audioSource.Play();
    }
}
