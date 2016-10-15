using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float speed;
    public float jumpSpeed;
    public Vector3 speedNow;

	private Vector3 moveDirection;
    private float horizontalMovement;
    private float verticalMovement;
    private bool jumping = false;

    private Rigidbody rb;

    void Start() {
        rb = this.gameObject.GetComponent<Rigidbody>();

    }

    void Update() {
        if (rb.velocity == Vector3.zero) {
            Time.timeScale = 0.1f;
        } else {
            Time.timeScale = 1;
        }
    }

    void FixedUpdate() {
        MovementControl();
        Jump();
    }

    void OnCollisionEnter(Collision collision) {
        CollideGround(collision);
    }



    void MovementControl() {

        if (Input.GetAxis("Horizontal") != 0 && !jumping) {
            if (Input.GetAxis("Horizontal") > 0) {
                horizontalMovement = 1f;
            } else {
                horizontalMovement = -1f;
            }
        } else {
            horizontalMovement = 0f;
        }

        if (Input.GetAxis("Vertical") != 0 && !jumping) {
            if (Input.GetAxis("Vertical") > 0) {
                verticalMovement = 1f;
            } else {
                verticalMovement = -1f;
            }
        } else {
            verticalMovement = 0f;
        }

        moveDirection = new Vector3(horizontalMovement, 0f, verticalMovement);

        if (!jumping) {
            rb.velocity = new Vector3((moveDirection.normalized * speed).x, rb.velocity.y, (moveDirection.normalized * speed).z);
        }
    }

    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && !jumping) {
            Debug.Log("Jump called");
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
            jumping = true;
        }
    }

    void CollideGround(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            jumping = false;
        }
    }

}
