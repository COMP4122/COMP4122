using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float maxSpeed;
    public float jumpSpeed;

    private Vector3 moveDirection;
    private bool jumping = false;

    private Rigidbody rb;

    void Start() {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        HorizontalMovement();
        Jump();
    }

    void HorizontalMovement() {
        // disable horizontal movement control while jumping
        if (!jumping) {
            // only modify movement why key is pressed, otherwise free movement
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveDirection = moveDirection.normalized;

            rb.velocity = new Vector3((moveDirection.normalized * maxSpeed).x, rb.velocity.y, (moveDirection.normalized * maxSpeed).z);
        }
    }

    // jumping is controlled with velocity directly
    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping) {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
            jumping = true;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            jumping = false;
        }
    }
}
