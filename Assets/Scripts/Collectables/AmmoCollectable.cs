using UnityEngine;

public class AmmoCollectable : MonoBehaviour {

    public float spinSpeed;
    public int minAmmoAmt;
    public int maxAmmoAmt;

    void FixedUpdate() {
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PlayerShoot>().AddAmmo(Random.Range(minAmmoAmt, maxAmmoAmt));
            Destroy(this.gameObject);
        }
    }
}
