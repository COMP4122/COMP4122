using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigFollowCamera : MonoBehaviour {

    public GameObject mainCamera;
    private bool followingCamera;


    void Update () {
        transform.rotation = mainCamera.transform.rotation;
        transform.Rotate(transform.forward, -90f, Space.World);
    }
}
