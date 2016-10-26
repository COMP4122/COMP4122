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
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour {

    public BasicBullet basicBullet;
    public Text ammoText;

    private int basicBulletAmt = 5;

    void Start() {
        UpdateAmmoText();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && HasAmmo()) {
            ShootBasicBullet();
        }
    }

    bool HasAmmo() {
        return basicBulletAmt > 0;
    }

    public void AddAmmo(int amt) {
        basicBulletAmt += amt;
        UpdateAmmoText();

        // TODO: shit code here
        Player player = GetterUtility.GetPlayer();
        player.Heal(amt * 2);
    }

    void UpdateAmmoText() {
        ammoText.text = "Ammo: " + basicBulletAmt;
        if (basicBulletAmt <= 0) {
            ammoText.text = "No Ammo!";
        }
    }

    void ShootBasicBullet() {
        RaycastHit hitOnFloor = MouseUtility.RayCastToFloor();
        BasicBullet newBasic = Instantiate(basicBullet).GetComponent<BasicBullet>();
        newBasic.ShootWithRay(GetShootRay(hitOnFloor));
        basicBulletAmt--;
        UpdateAmmoText();
    }

    Ray GetShootRay(RaycastHit hit) {
        Vector3 pointAboveFloor = hit.point + new Vector3(0, this.transform.position.y, 0);
        Vector3 shootDirection = pointAboveFloor - transform.position;
        Ray shootRay = new Ray(transform.position + 1f * shootDirection.normalized, shootDirection);
        return shootRay;
    }

    Ray GetShootRay(Vector3 targetPosition) {
        Vector3 shootDirection = targetPosition - transform.position;
        Ray shootRay = new Ray(transform.position + 1f * shootDirection.normalized, shootDirection);
        return shootRay;
    }
}
