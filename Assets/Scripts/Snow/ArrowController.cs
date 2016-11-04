using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {
    public float maxSpeed = 25.0f, disapearTime = 10.0f, translateValueAfterHit = 0.15f;
    public bool keepRigidbodyAfterHit;
    private Rigidbody rb;
    private Collider collider;
    private bool stoped;

    void SetSpeed(float speed) {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;
    }

	void Start () {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        stoped = false;
	}
	
	void Update () {
        if (!stoped) {
            float rotate = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.x, -170.0f, 0.5f) - transform.rotation.eulerAngles.x;
            transform.Rotate(new Vector3(rotate, 0.0f, 0.0f));
        }
	}

    void OnCollisionEnter(Collision collision) {
        if (stoped || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Arrow" || collision.gameObject.tag == "Invisible")
            return;
        if (collision.gameObject.tag == "Hitable") {
            
        }
        stoped = true;
        if (!keepRigidbodyAfterHit) {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            collider.enabled = false;
            transform.Translate(Vector3.up * translateValueAfterHit);
            transform.SetParent(collision.gameObject.transform);
        }
        StartCoroutine(disapear());
    }

    IEnumerator disapear() {
        yield return new WaitForSeconds(disapearTime);
        Destroy(gameObject);
    }
}
