using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour {


    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool("PickingUpObjects", false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            StartCoroutine(PickUpObjectCoroutine());
        }
    }

    IEnumerator PickUpObjectCoroutine() {
        animator.SetBool("PickingUpObjects", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("PickingUpObjects", false);
        StopCoroutine("PickUpObjectCoroutine");
    }

}