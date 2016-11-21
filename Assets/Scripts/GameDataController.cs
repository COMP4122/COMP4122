using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDataController : MonoBehaviour {
    public static GameDataController singleton;
    public string serverURL;

    public GameObject canvas, playerCanvas;
    public Slider slider, slider2;
    public Text saveText;
    public InputField nameInput;
    private Watcher watcher;
    private GameData data;
    private const int questNumber = 6;

	void Awake () {
        if (singleton == null) {
            DontDestroyOnLoad(gameObject);
            singleton = this;
        }
        else if (singleton != this) {
            Destroy(gameObject);
        }
	}

    void Start() {
        nameInput.gameObject.SetActive(false);
        canvas.SetActive(false);
        playerCanvas.SetActive(false);
    }

    public void NewGame() {
        InitData();
        playerCanvas.SetActive(true);
        SceneManager.LoadScene(data.sceneName);
    }

    public void Save(GameData newData) {
        data = newData;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(getSaveFilePath());
        bf.Serialize(file, data);
        file.Close();
        StartCoroutine(SaveSuccess());
    }

    public void Load() {
        if (File.Exists(getSaveFilePath())) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(getSaveFilePath(), FileMode.Open);
            data = (GameData)bf.Deserialize(file);
            file.Close();
            playerCanvas.SetActive(true);
            SceneManager.LoadScene(data.sceneName);
        }
    }

    public void Exit() {
        Application.Quit();
    }

    public void GameOver() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player.activeSelf) {
			player.SetActive (false);
		}
        GameObject dieCamera = new GameObject();
        dieCamera.AddComponent<Camera>();
        dieCamera.AddComponent<AudioListener>();
        dieCamera.transform.position = player.transform.position;
        dieCamera.transform.Translate(0f, 10f, 0f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canvas.SetActive(true);
        playerCanvas.SetActive(true);
        nameInput.gameObject.SetActive(true);
    }

    public void OnEnteredName() {
        if (nameInput.text == "")
            return;
        string name = nameInput.text;
        nameInput.text = "";
        StartCoroutine(GameOverRoutine(name));
    }

    public void ChangeScene(GameData newData, string sceneName) {
        data = newData;
        if (sceneName == "Camp") {
            data.sceneName = "Camp";
            data.position[0] = 35f;
            data.position[1] = 0f;
            data.position[2] = 44f;
            data.dayCount++;
            if (data.meatCount < data.survivorCount * 5)
                data.survivorCount--;
            data.meatCount = 0;
        }
        SceneManager.LoadScene(sceneName);

    }
    
    public string getSaveFilePath() {
        return Application.persistentDataPath + "/save1.save";
    }

    public void SetData(GameData newData) {
        data = newData;
    }

    public GameData getData() {
        if (data == null)
            Load();
        return data;
    }

    public void setWatcher(Watcher watcher) {
        this.watcher = watcher;
    }

    public void CmdSave() {
        watcher.Save();
    }

    public void GUIResume() {
        watcher.DisableMenu();
    }

    private IEnumerator GameOverRoutine(string name) {
        watcher.Save();
        int score = data.totalNumberOfMeat * 1 + data.dayCount * 5 + data.survivorCount * 10;
        WWW www = new WWW(serverURL + "?name=" + name + "&score=" + score);
        yield return www;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
        canvas.SetActive(false);
    }

    IEnumerator SaveSuccess() {
        saveText.text = "Game Saved";
        yield return new WaitForSecondsRealtime(3);
        saveText.text = "";
    }

    private void InitData() {
        data = new GameData();

        data.sceneName = "Camp";
        data.health = 100f;
        data.WeaponMode = ShootMode.Rock;
        data.dayCount = 0;
        data.meatCount = 0;
        data.survivorCount = 3;
        data.totalNumberOfMeat = 0;
        data.questList = InitQuest();

        data.position = new float[3];
        data.position[0] = 10f;
        data.position[1] = 0f;
        data.position[2] = -70f;

        data.rotation = new float[4];
        data.rotation[0] = 0f;
        data.rotation[1] = 0f;
        data.rotation[2] = 0f;
        data.rotation[3] = 1f;
    }

    private int[] InitQuest() {
        int[] list = new int[questNumber];
        for (int i = 0; i < list.Length; i++)
            list[i] = 0;
        list[0] = 1;
        return list;
    }
}
