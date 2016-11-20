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
        
        if (collider.gameObject.tag == "Item") {
            if (Input.GetKeyDown(KeyCode.F)) {
				Item itemToPickUp = collider.gameObject.GetComponent<Item>();
				StartCoroutine(PickUpObjectCoroutine(itemToPickUp));
            }
        }
    }

    IEnumerator PickUpObjectCoroutine(Item item) {
        animator.SetBool("PickingUpObjects", true);
        yield return new WaitForSeconds(0.5f);

		item.gameObject.transform.SetParent (GameObject.Find ("ArmR2").transform);
		if (item.gameObject.GetComponent<Animator> () != null) {
			item.gameObject.GetComponent<Animator> ().enabled = false;
		}

        yield return new WaitForSeconds(1f);

		item.GotPickedUp();

        animator.SetBool("PickingUpObjects", false);
        StopCoroutine("PickUpObjectCoroutine");
    }

}