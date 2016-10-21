using UnityEngine;
public class EnemyBullet : BasicBullet{

    public float hitSpeed;

    void Start() {
        Destroy(this.gameObject, 5f);
    }
    void FixedUpdate() {
        Move();
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            collision.gameObject.GetComponent<Player>().GetHitFrom(transform.position, hitSpeed);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Friend")
        {
            collision.gameObject.GetComponent<FriendShooter>().Die();
            Destroy(this.gameObject);
        }
    }
}
