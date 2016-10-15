using UnityEngine;

public class BasicBullet : MonoBehaviour {

    public float damage;
    public float speed;

    private Vector3 shootDirection;

    void FixedUpdate() {
        transform.Translate(shootDirection.normalized * speed * Time.deltaTime, Space.World);
    }

	public void ShootToRay(Ray shootRay) {
        this.shootDirection = shootRay.direction;
        this.transform.position = shootRay.origin;
        //RotateToShootDirection();
        Debug.Log("Actual shoot direction: " + shootDirection);
    }

    void RotateToShootDirection() {
        Vector3 newRotation = Vector3.RotateTowards(transform.forward, shootDirection, 0.01f, 0.0f);
        transform.rotation = Quaternion.LookRotation(newRotation, Vector3.up);
    }
}
