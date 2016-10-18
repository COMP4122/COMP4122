using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float maxSpeed;
    public float jumpSpeed;
    public Vector3 speedNow;
    public float moveForce;

	private Vector3 moveDirection;
    private float horizontalMovement;
    private float verticalMovement;
    private bool jumping = false;

    private Rigidbody rb;

    void Start() {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update() {
//        if (rb.velocity == Vector3.zero) {
//            Time.timeScale = 0.01f;
//        } else {
//            Time.timeScale = 1;
//        }
    }

    void FixedUpdate() {
        MovementControlWithVelocity();
        Jump();
    }

    void OnCollisionEnter(Collision collision) {
        CollideGround(collision);
    }



    //TODO: maybe change this later to control with force instead of velocity
    void MovementControl() {
        // disable horizontal movement control while jumping
        if (!jumping) {

            // receive horizontal inputs
            if (Input.GetAxis("Horizontal") != 0 && !jumping) {
                if (Input.GetAxis("Horizontal") > 0) {
                    horizontalMovement = 1f;
                } else {
                    horizontalMovement = -1f;
                }
            } else {
                horizontalMovement = 0f;
            }

            // "vertical" here actually means z axis movement
            if (Input.GetAxis("Vertical") != 0 && !jumping) {
                if (Input.GetAxis("Vertical") > 0) {
                    verticalMovement = 1f;
                } else {
                    verticalMovement = -1f;
                }
            } else {
                verticalMovement = 0f;
            }

            // move direction
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveDirection = moveDirection.normalized;

            // add force
            rb.AddForce(moveDirection * moveForce);
            Debug.Log("Force added: " + moveDirection * moveForce);

            if (rb.velocity.magnitude > maxSpeed) {
                rb.velocity = rb.velocity * (maxSpeed / rb.velocity.magnitude); // keep speed under max
            }
            Debug.Log("Speed: " + rb.velocity.magnitude);
            // rb.velocity = new Vector3((moveDirection.normalized * speed).x, rb.velocity.y, (moveDirection.normalized * speed).z);
        }
    }

    void MovementControlWithVelocity() {
        // disable horizontal movement control while jumping
        if (!jumping) {

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

    // reset jumping
    void CollideGround(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            jumping = false;
        }
    }

}
