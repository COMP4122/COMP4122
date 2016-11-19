using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigFollowCamera : MonoBehaviour {

    public GameObject camera;
    private bool followingCamera;


    void Update () {
        Debug.Log(camera.transform.rotation);
        transform.rotation = camera.transform.rotation;
        transform.Rotate(transform.forward, -90f, Space.World);
    }
}
