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
            Debug.Log("Something is wrong with PlayerShoot:RayCastToFloor");
            return hitPointOnFloor;
        }
    }

    Ray GetShootRay(RaycastHit hit) {
        Vector3 pointAboveFloor = hit.point + new Vector3(0, this.transform.position.y, 0);
        Vector3 shootDirection = pointAboveFloor - transform.position;

        Ray shootRay = new Ray(transform.position + 1f * shootDirection.normalized, shootDirection);

        return shootRay;
    }
}