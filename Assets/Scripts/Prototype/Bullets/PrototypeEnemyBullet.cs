using UnityEngine;
public class PrototypeEnemyBullet : PrototypeBasicBullet {

    public float hitSpeed;

    void Start() {
        Destroy(this.gameObject, 5f);
    }
    void FixedUpdate() {
        Move();
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PrototypePlayer>().TakeDamage(damage);
            collision.gameObject.GetComponent<PrototypePlayer>().GetHitFrom(transform.position, hitSpeed);
            Destroy(this.gameObject);
        }
    }
}
