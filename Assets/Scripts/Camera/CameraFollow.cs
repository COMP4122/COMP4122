using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float distance;
    public float verticalAngle;
    public float horizontalAngle;
    public Transform followObject;

    #region rotation variable
    private float horizontalDistance = 0;
    private float xOffset;
    private float yOffset;
    private float zOffset;

    #endregion
    void Update() {
        
    }

	void LateUpdate () {
        MoveToTarget();
        LookAtTarget();
	}

    void MoveToTarget() {
        yOffset = distance * Mathf.Sin(Mathf.Deg2Rad * verticalAngle);
        horizontalDistance = distance * Mathf.Cos(Mathf.Deg2Rad * verticalAngle);
        xOffset = horizontalDistance * Mathf.Sin(Mathf.Deg2Rad * horizontalAngle);
        zOffset = -horizontalDistance * Mathf.Cos(Mathf.Deg2Rad * horizontalAngle);
        this.transform.position = new Vector3(
                followObject.position.x + xOffset,
                followObject.position.y + yOffset,
                followObject.position.z + zOffset
        );
    }

    void LookAtTarget() {
        this.transform.LookAt(followObject);
    }
}
