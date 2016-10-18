using UnityEngine;

public class MouseUtility : MonoBehaviour {

    public static LayerMask mask = LayerMask.GetMask("Ground");

    public static RaycastHit RayCastToFloor() {
        RaycastHit hitPointOnFloor;
        Ray rayToFloor = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayToFloor, out hitPointOnFloor, 100.0f, mask, QueryTriggerInteraction.Ignore)) {
            // Debug.Log("RayToFloor: " + rayToFloor);
            // Debug.Log("Hit point of floor: " + hitPointOnFloor.point);
            return hitPointOnFloor;
        } else {
            Debug.Log("Something is wrong with MouseUtility:RayCastToFloor");
            return hitPointOnFloor;
        }
    }

    public static Vector3 GetRayCastToFloorPoint(float height) {
        RaycastHit hitPointOnFloor;
        Ray rayToFloor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 position = Vector3.zero;

        if (Physics.Raycast(rayToFloor, out hitPointOnFloor, 100.0f, mask, QueryTriggerInteraction.Ignore)) {
            position = hitPointOnFloor.point + new Vector3(0, height, 0);
            return position;
        } else {
            Debug.Log("Something is wrong with MouseUtility:RayCastToFloorGetPoint");
            return position;
        }
    }
}