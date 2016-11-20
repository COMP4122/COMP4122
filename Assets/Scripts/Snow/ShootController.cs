using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShootController : MonoBehaviour {
    public GameObject arrow, stone;
    public Text weaponText, strengthText;
    public Transform shootSpawn;
    public float fireRate = 1.0f, speedOfDragBow = 0.025f;
	private Camera mainCamera;
    private float nextFire, strength;
    private int weapon; // 0 - Stone, 1 - Arrow
    private int typeOfWeapon;

    void Start() {
        nextFire = 0.0f;
        strength = 0.0f;
        weapon = 0;
        typeOfWeapon = 2;
        mainCamera = Camera.main;
    }

    void Update() {
        // Shoot
        if (Input.GetMouseButtonUp(0) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Vector3 position = shootSpawn.position;
            Vector3 euler = shootSpawn.rotation.eulerAngles;
            Quaternion rotation = Quaternion.Euler(euler.x + 90.0f, euler.y, euler.z);
            if (weapon == 0) {
                GameObject stoneObject = Instantiate(stone, position, rotation) as GameObject;
                ArrowController ac = stoneObject.GetComponent<ArrowController>();
                stoneObject.SendMessage("SetSpeed", ac.maxSpeed);
            }
            else if (weapon == 1) {
                GameObject arrowObject = Instantiate(arrow, position, rotation) as GameObject;
                ArrowController ac = arrowObject.GetComponent<ArrowController>();
                arrowObject.SendMessage("SetSpeed", ac.maxSpeed * strength);
                strength = 0.0f;
            }
        }
        // Drag bow or aiming
        if (Input.GetMouseButton(0) && Time.time > nextFire) {
            if (weapon == 1) {
                strength = Mathf.MoveTowards(strength, 1.0f, speedOfDragBow);
                strengthText.text = "Strength: " + Mathf.Round(strength * 100.0f);
            }
        }
        // Reset drag
        if (!Input.GetMouseButton(0) && weapon == 1) {
            strengthText.text = "Strength: 0";
        }
        // Zoom in
        if (Input.GetMouseButton(1)) {
            mainCamera.fieldOfView = Mathf.MoveTowards(mainCamera.fieldOfView, 40.0f, 1.0f);
        }
        // Zoom out
        if (!Input.GetMouseButton(1)) {
            mainCamera.fieldOfView = Mathf.MoveTowards(mainCamera.fieldOfView, 60.0f, 1.0f);
        }
        // Change weapon
        if (Input.mouseScrollDelta.y != 0.0f) {
            if (Input.mouseScrollDelta.y > 0)
                weapon = (weapon + (int)(Input.mouseScrollDelta.y)) % typeOfWeapon;
            else {
                int offset = (int)Input.mouseScrollDelta.y;
                while (offset < 0)
                    offset += typeOfWeapon;
                weapon = (weapon + offset) % typeOfWeapon;
            }
            strength = 0.0f;
            if (weapon == 0) {
                weaponText.text = "Weapon Mode: Stone";
                strengthText.text = "";
            }
            else if (weapon == 1) {
                weaponText.text = "Weapon Mode: Arrow";
                strengthText.text = "Strength: 0";
            }
        }
    }
}
