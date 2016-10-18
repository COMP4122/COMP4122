using UnityEngine;
using System.Collections;

public class Trigger1 : MonoBehaviour {

    public GameObject[] enemies;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player") {
            foreach (GameObject enemy in enemies) {
                enemy.SetActive(true);
            }
        }
    }
}
