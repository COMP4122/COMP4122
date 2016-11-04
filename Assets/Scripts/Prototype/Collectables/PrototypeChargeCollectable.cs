using UnityEngine;
using System.Collections;

public class PrototypeChargeCollectable : MonoBehaviour {

    //TODO: make an abstract class for collectables
    public float spinSpeed;
    public int minAmmoAmt;
    public int maxAmmoAmt;

    void FixedUpdate() {
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<PrototypePlayerMovement>().AddChargeAmt(Random.Range(minAmmoAmt, maxAmmoAmt));
            Destroy(this.gameObject);
        }
    }
}
