using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public float maxSpeed;
    public float jumpSpeed;

    public int chargeAmt = 3;
    public Text chargeText;

	private Vector3 moveDirection;
    private bool jumping = false;
    private bool movementDisabled = false;
    private bool blinking = false;

    private Rigidbody rb;
    private TrailRenderer trailRenderer;

    void Start() {
        rb = this.gameObject.GetComponent<Rigidbody>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.startWidth = 0f;
        UpdateChargeText();
    }

    void Update() {
//        if (rb.velocity == Vector3.zero) {
//            Time.timeScale = 0.01f;
//        } else {
//            Time.timeScale = 1;
//        }
    }

    void FixedUpdate() {
        HorizontalMovement();
        Jump();
        Blink();
    }

    void HorizontalMovement() {
        // disable horizontal movement control while jumping
        if (!jumping && !movementDisabled) {
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

    void Blink() {
        if (Input.GetKeyDown(KeyCode.B) && !blinking && chargeAmt > 0) {
            chargeAmt--;
            UpdateChargeText();
            StopCoroutine("BlinkCoroutine");
            blinking = true;
            SetVelocityToZero();
            StartCoroutine(BlinkCoroutine());
        }
    }

    IEnumerator BlinkCoroutine() {
        float blinkDuration = 0.2f;
        float blinkDistance = 20f;
        float blinkSpeed = blinkDistance/blinkDuration;
        float blinkStartTime = Time.time;

        Vector3 targetPosition = MouseUtility.GetRayCastToFloorPoint(transform.position.y);
        Vector3 blinkDirection = (targetPosition - transform.position).normalized;

        DisableMovementFor(blinkDuration);

        trailRenderer.startWidth = 1f;
        while (Time.time - blinkStartTime < blinkDuration) {
            transform.Translate(0.02f * blinkSpeed * blinkDirection, Space.World);
            yield return new WaitForSeconds(0.02f);
        }

        trailRenderer.startWidth = 0f;
        blinking = false;
    }

    #region collisions
    void OnCollisionEnter(Collision collision) {
        CollideGround(collision);
    }

    // reset jumping
    void CollideGround(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            jumping = false;
        }
    }
    #endregion

    void SetVelocityToZero() {
        rb.velocity = Vector3.zero;
    }

    public void AddChargeAmt(int amt) {
        chargeAmt += amt;
        UpdateChargeText();
    }

    void UpdateChargeText() {
        chargeText.text = "Charge: " + chargeAmt;

        if (chargeAmt <= 0) {
            chargeText.text = "No charge available!";
        }
    }

    public void DisableMovementFor (float seconds) {
        StopCoroutine("DisableMovementCoroutine");
        StartCoroutine(DisableMovementCoroutine(seconds));
    }

    IEnumerator DisableMovementCoroutine(float seconds) {
        movementDisabled = true;
        yield return new WaitForSeconds(seconds);
        movementDisabled = false;
    }

    public void GetHitFrom(Vector3 hitPosition, float hitSpeed) {
        Vector3 hitDirection = transform.position - hitPosition;
        hitDirection = hitDirection.normalized;
        rb.velocity = hitDirection * hitSpeed;
        DisableMovementFor(.5f);
    }

}
