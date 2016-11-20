using UnityEngine;

public class PrototypeBasicBullet : MonoBehaviour {

    public float damage;
    public float speed;
    public bool canPenetrate = true;

    private Vector3 shootDirection;

    void Start() {
        Destroy(this.gameObject, 5f);
    }
    void FixedUpdate() {
        Move();
    }

    protected void Move() {
        transform.Translate(shootDirection.normalized * speed * Time.deltaTime, Space.World);
    }

	public void ShootWithRay(Ray shootRay) {
        this.shootDirection = shootRay.direction;
        this.transform.position = shootRay.origin;
        //TODO: RotateToShootDirection();
    }

    void RotateToShootDirection() {
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, shootDirection, 0.01f, 0.0f);
        transform.rotation = Quaternion.LookRotation(newRotation, Vector3.up);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<PrototypeEnemyShooter>().Die();
            if (!canPenetrate)
                Destroy(this.gameObject);
        }
    }
}
