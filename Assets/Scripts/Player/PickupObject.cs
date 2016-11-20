using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour {


    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool("PickingUpObjects", false);
    }

    void OnTriggerStay(Collider collider) {
        
        if (collider.gameObject.tag == "Meat") {
            if (Input.GetKeyDown(KeyCode.F)) {
                Meat meat = collider.gameObject.GetComponent<Meat>();
                StartCoroutine(PickUpObjectCoroutine(meat));
            }
        }
    }

    IEnumerator PickUpObjectCoroutine(Meat meat) {
        animator.SetBool("PickingUpObjects", true);
        yield return new WaitForSeconds(0.5f);

		meat.gameObject.transform.SetParent (GameObject.Find ("ArmR2").transform);
        

        yield return new WaitForSeconds(1f);

		meat.GotPickedUp();

        animator.SetBool("PickingUpObjects", false);
        StopCoroutine("PickUpObjectCoroutine");
    }

}