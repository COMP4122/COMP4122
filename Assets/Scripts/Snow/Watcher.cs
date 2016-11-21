using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Watcher : MonoBehaviour {
    public float autoSaveDuration;
    public string sceneName;

    private GameDataController dc;  // Used to save/load/retrieve data
    private GameData data;  // See GameData.cs in DataStructure folder
    private GameObject player;
    private GameObject dieCamera;
    private GameObject menu;

    private bool playerDie = false, menuUp = false;
    private Vector3 dieCameraPosition;
    private Quaternion dieCameraRotation;

    void Start() {
        dc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameDataController>();
        player = GameObject.FindGameObjectWithTag("Player");
        menu = dc.canvas;
        menu.SetActive(false);
        data = dc.getData();
        // When loading to a new scene from camp
        if (data.sceneName != sceneName) {
            data.sceneName = sceneName;
        }
        // When loading from save file
        else {
            Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
            Quaternion rotation = new Quaternion(data.rotation[1], data.rotation[2], data.rotation[3], data.rotation[0]);
            player.transform.position = position;
            player.transform.rotation = rotation;
        }
        Time.timeScale = 1.0f;
        dc.setWatcher(this);
    }

    void Update() {
        if (!playerDie && Input.GetKeyDown(KeyCode.Escape)) {
            SwitchMenu();
        }
        if (!playerDie && Input.GetKeyDown(KeyCode.F5)) {
            dc.Save(data);
        }
        if (!playerDie && Input.GetKeyDown(KeyCode.F9)) {
            dc.Load();
        }
        if (playerDie) {
            dieCamera.transform.position = Vector3.Lerp(dieCamera.transform.position, dieCameraPosition, 0.01f);
            dieCamera.transform.Rotate(new Vector3(0.35f, 0f, 0f));
        }
    }

    public void ChangeScene(string sceneName) {
        dc.ChangeScene(data, sceneName);
    }

    public void SetHealth(float health) {
        if (data == null)
            data = dc.getData();
        data.health = health;
        if (health <= 0) {
            StartCoroutine(PlayerDie());
        }
    }

    public void addMeat(int number) {
        if (data == null)
            data = dc.getData();
        data.meatCount += number;
        data.totalNumberOfMeat += number;
    }

    public void subMeat(int number) {
        if (data == null)
            data = dc.getData();
        data.meatCount -= number;
    }

    public void SetWeaponMode(ShootMode mode) {
        dc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameDataController>();
        if (data == null)
            data = dc.getData();
        data.WeaponMode = mode;
    }

    public ShootMode getShootMode() {
        return dc.getData().WeaponMode;
    }

    public void SwitchMenu() {
        if (menuUp)
            DisableMenu();
        else
            EnableMenu();
    }

    public void EnableMenu() {
        menuUp = true;
        Time.timeScale = 0.01f;
        menu.SetActive(true);
        Component[] list = player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in list)
            script.enabled = false;
        Cursor.visible = true;
    }

    public void DisableMenu() {
        menuUp = false;
        Time.timeScale = 1f;
        menu.SetActive(false);
        Component[] list = player.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in list)
            script.enabled = true;
        Cursor.visible = false;
    }

    IEnumerator PlayerDie() {
        player.SetActive(false);
        dieCamera = new GameObject();
        dieCamera.AddComponent<Camera>();
        dieCamera.AddComponent<AudioListener>();
        dieCamera.transform.position = player.transform.position;
        dieCamera.transform.Translate(0f, 20f, 0f);
        dieCameraPosition = dieCamera.transform.position;
        dieCamera.transform.Translate(0f, -20f, 0f);
        playerDie = true;
        yield return new WaitForSeconds(5);
        data.meatCount = 0;
        data.health = 100f;
        data.survivorCount -= 1;
        dc.ChangeScene(data, "Camp");
    }

	public int GetSurvivorCount() {
		if (data != null) {
			Debug.Log ("data is not null");
			return data.survivorCount;
		} else {
			return 3;
		}
	}

    public void Save() {
        data.position[0] = player.transform.position.x;
        data.position[1] = player.transform.position.y;
        data.position[2] = player.transform.position.z;
        data.rotation[0] = player.transform.rotation.w;
        data.rotation[1] = player.transform.rotation.x;
        data.rotation[2] = player.transform.rotation.y;
        data.rotation[3] = player.transform.rotation.z;
        dc.Save(data);
    }
}
