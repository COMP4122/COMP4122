using System.Collections;
using UnityEngine;
public class Projectile : MonoBehaviour{

    public float damage;
    public enum ProjectileType {Rock, Arrow}
    public ProjectileType type = ProjectileType.Arrow;
    public AudioClip arrowHitAudio, dropInWaterAudio;
    public bool canDealDamage = true;

    private bool playedSound = false;
    private AudioSource audioSource;
    private float flySpeed;
    private Vector3 direction;
    private Rigidbody rb;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetFlySpeed(float speed) {
        this.flySpeed = speed;
    }

    public void SetTarget(Vector3 targetPos) {
        direction = targetPos - transform.position;
        direction = direction.normalized;
    }

    public void ThrowOut() {
        rb.velocity = direction * flySpeed;
    }

    void OnCollisionEnter(Collision collision) {
		
        if (collision.gameObject.tag != "Player") {


            GetComponent<Rigidbody>().velocity = Vector3.zero;

			if (canDealDamage) {
				
				if (collision.gameObject.tag == "Enemy") {
					GetComponent<Rigidbody> ().useGravity = false;
					GetComponent<Rigidbody> ().isKinematic = true;
					transform.SetParent (collision.gameObject.transform);

					if (!playedSound) {
						audioSource.clip = arrowHitAudio;
						audioSource.Play ();
						playedSound = true;
					}

					AnimalController animal = collision.gameObject.GetComponent<AnimalController> ();

					animal.TakeDamage (this.damage);

					DisableCollider ();
				}

			}

			canDealDamage = false;
        }
    }

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag != "Player") {
			if (canDealDamage) { 
				if (collider.gameObject.tag == "Water") {
					GetComponent<Rigidbody> ().useGravity = true;
					GetComponent<Rigidbody> ().velocity = GetComponent<Rigidbody> ().velocity.normalized * 10f;


					if (!playedSound) {
						audioSource.clip = dropInWaterAudio;
						audioSource.Play ();
						playedSound = true;
					}
				}
			}
		}
	}

	void DisableCollider() {
		if (type == ProjectileType.Arrow) {

			this.transform.GetChild (1).GetComponent<Collider>().enabled = false;
		
		}
	}
}