﻿using UnityEngine;
using System.Collections;

public class OldPlayerMovement : MonoBehaviour {

    public float maxSpeed;
    public float jumpSpeed;
    public float gravityMagnitude;

    private Vector3 moveDirection;
    private bool jumping = false;

    private Rigidbody rb;

    void Start() {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        HorizontalMovement();
        Jump();
        ManualGravity();
        ManualJumpChecking();
    }

    void HorizontalMovement() {
        // disable horizontal movement control while jumping
        if (!jumping) {
            // only modify movement why key is pressed, otherwise free movement
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveDirection = moveDirection.normalized;

            rb.velocity = new Vector3((moveDirection.normalized * maxSpeed).x, rb.velocity.y, (moveDirection.normalized * maxSpeed).z);
        }
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            LookAtMovingDirection();
    }

    // jumping is controlled with velocity directly
    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping) {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
            jumping = true;
        }
    }

    void LookAtMovingDirection() {
        Vector3 positionToLookAt = new Vector3(
                transform.position.x + rb.velocity.normalized.x,
                transform.position.y,
                transform.position.z + rb.velocity.normalized.z);
        transform.LookAt(positionToLookAt);
    }

    void ManualGravity() {
        rb.AddForce(Vector3.down * gravityMagnitude);
    }

    void ManualJumpChecking() {
        if (rb.velocity.y == 0f) {
            jumping = false;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            jumping = false;
        }
    }
}
