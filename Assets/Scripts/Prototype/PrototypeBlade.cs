using UnityEngine;
using System.Collections;

public class PrototypeBlade : MonoBehaviour {

    public float spinSpeed = 180f;
    public Transform target;

	void FixedUpdate() {
        transform.RotateAround(target.transform.position, target.transform.up, spinSpeed * Time.deltaTime);
    }

    public void SetAwakeFor (float seconds) {
        this.gameObject.SetActive(true);
        StopCoroutine("SetAwake");

        StartCoroutine(SetAwake(seconds));
    }

    IEnumerator SetAwake(float seconds) {

        yield return new WaitForSeconds(seconds);
        this.gameObject.SetActive(false);

    }

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Enemy") {
            collider.gameObject.GetComponent<PrototypeEnemyShooter>().Die();
        }
    }
}
