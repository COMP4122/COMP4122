using System.Collections;
using UnityEngine;
public class Projectile : MonoBehaviour{

    public float damage;

    private float flySpeed;
    private Vector3 direction;
    private bool flying = false;
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
        flying = true;
        rb.velocity = direction * flySpeed;
    }
}