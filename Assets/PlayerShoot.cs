/*
 * Copyright (c) 2015 Razeware LLC
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public BasicBullet basicBullet;
    public LayerMask mask;

    void Update() {
        bool mouseButtonDown = Input.GetMouseButtonDown(0);
        if (mouseButtonDown) {
            RaycastHit hitPointOnFloor = RayCastToFloor();

            ShootBasicBulletTo(GetShootRay(hitPointOnFloor));
            Debug.Log("Target: " + GetShootRay(hitPointOnFloor));
        }
    }

    RaycastHit RayCastToFloor() {
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

    void ShootBasicBulletTo(Ray ray) {
        BasicBullet newBasic = Instantiate(basicBullet).GetComponent<BasicBullet>();
        newBasic.ShootToRay(ray);
    }

    Ray GetShootRay(RaycastHit hit) {
        Vector3 pointAboveFloor = hit.point + new Vector3(0, this.transform.position.y, 0);
        Vector3 shootDirection = pointAboveFloor - transform.position;

        Ray shootRay = new Ray(this.gameObject.transform.position, shootDirection);

        return shootRay;
    }
}
