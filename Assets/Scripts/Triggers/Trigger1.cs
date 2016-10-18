using UnityEngine;
using System.Collections;

public class Trigger1 : MonoBehaviour {

    public GameObject[] enemies;

    private bool activated = false;

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Player" && !activated) {
            activated = true;
            foreach (GameObject enemy in enemies) {
                enemy.SetActive(true);
            }
        }
    }
}
