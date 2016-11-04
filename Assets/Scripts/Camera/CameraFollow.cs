using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public float distance;
    public float verticalAngle;
    public float horizontalAngle;
    public Transform followObject;
    public float rotateSpeed = 40f;

    #region rotation variable
    private float horizontalDistance = 0;
    private float xOffset;
    private float yOffset;
    private float zOffset;

    #endregion
    void Update() {
        RotateCamera();
    }

    void RotateCamera() {
        if (Input.GetKey(KeyCode.LeftBracket)) {
            verticalAngle -= rotateSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightBracket)) {
            verticalAngle += rotateSpeed * Time.deltaTime;
        }
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
