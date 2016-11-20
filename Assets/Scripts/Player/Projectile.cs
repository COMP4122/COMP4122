using System.Collections;
using UnityEngine;
public class Projectile : MonoBehaviour{

    public float damage;
    public enum ProjectileType {Rock, Arrow}
    public ProjectileType type = ProjectileType.Arrow;
    public AudioClip arrowHitAudio;
    public bool canDealDamage = true;

    private bool playedSound = false;
    private AudioSource audioSource;
    private float flySpeed;
    private Vector3 direction;
    private bool flying = false;
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
        flying = true;
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

					switch (type) {
					case ProjectileType.Arrow:
						if (!playedSound) {
							audioSource.clip = arrowHitAudio;
							audioSource.Play ();
							playedSound = true;
						}
						break;
					default:
						break;
					}

					AnimalController animal = collision.gameObject.GetComponent<AnimalController> ();

					animal.TakeDamage (this.damage);
				}
			}

			canDealDamage = false;
        }

    }
}