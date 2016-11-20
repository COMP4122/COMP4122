using System;
using UnityEngine;

public class TestEnemy : MonoBehaviour {

	public float health = 200f;
    public GameObject meat;
    public AudioClip[] audioClips;

    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Projectile") {
            TakeDamage(collision.gameObject.GetComponent<Projectile>().damage);

            audioSource.clip = audioClips[0];
            audioSource.Play();

            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
            collision.gameObject.transform.SetParent(this.transform);
        }
    }

    void TakeDamage(float damage) {
        health -= damage;

        if (health <= 0)
            Die();
    }

    void Die() {
        Instantiate(meat, transform.position, transform.rotation);
        Destroy(this.gameObject, 2);
    }
}
