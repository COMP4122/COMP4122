using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float distance;
    public float angle;
    public Transform followObject;

	void LateUpdate () {
        MoveToTarget();
        LookAtTarget();
	}

    void MoveToTarget() {
        this.transform.position = new Vector3(
                followObject.position.x,
                followObject.position.y + distance * Mathf.Sin(Mathf.Deg2Rad * angle),
                followObject.position.z - distance * Mathf.Cos(Mathf.Deg2Rad * angle)
        );
    }

    void LookAtTarget() {
        this.transform.LookAt(followObject);
    }
}
