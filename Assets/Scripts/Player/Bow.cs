using System.Collections;
using UnityEngine;
public class Bow : MonoBehaviour{

    public float damage;

    private float flySpeed;
    private Vector3 direction;
    private Rigidbody rb;

    void Awake() {
        Destroy(this.gameObject, 10f);
        rb = GetComponent<Rigidbody>();
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
}