using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStateController : MonoBehaviour {
    public Text healthText;
    public float maxHealth;
    private float health;

	// Use this for initialization
	void Start () {
        health = maxHealth;
        healthText.text = "Health: " + Mathf.Round(health);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(float damage) {
        health -= damage;
        healthText.text = "Health: " + Mathf.Round(health);
    }
}
