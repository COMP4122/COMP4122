using UnityEngine;
using System.Collections;

public class CloudMover : MonoBehaviour {
    public float speed;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0.0f, 0.0f, 1.0f) * speed;
    }
	
	// Update is called once per frame
	void Update () {
    }
}
