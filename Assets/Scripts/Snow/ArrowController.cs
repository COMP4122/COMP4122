using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {
    public float maxSpeed = 25.0f, disappearTime = 10.0f, translateValueAfterHit = 0.15f, maxDamage;
    public bool keepRigidbodyAfterHit;
    public AudioClip shoot, land;

    private Rigidbody rb;
    private Collider collider;
    private AudioSource audioSource;
    private bool stoped;

    void SetSpeed(float speed) {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        audioSource.Play();
        rb.velocity = transform.up * speed;
    }

	void Start () {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        stoped = false;
	}
	
	void Update () {
        if (!stoped && rb.velocity.magnitude > 0.0f) {
            float rotate = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.x, -170.0f, 0.5f) - transform.rotation.eulerAngles.x;
            transform.Rotate(new Vector3(rotate, 0.0f, 0.0f));
        }
	}

    void OnCollisionEnter(Collision collision) {
        if (stoped || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Arrow" || collision.gameObject.tag == "Invisible")
            return;
        stoped = true;
        audioSource.clip = land;
        audioSource.Play();
        if (!keepRigidbodyAfterHit) {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            collider.enabled = false;
            transform.Translate(Vector3.up * translateValueAfterHit);
            transform.SetParent(collision.gameObject.transform);
        }
        Destroy(gameObject, disappearTime);
    }
}
