﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataController : MonoBehaviour {
    public static GameDataController singleton;
    public string serverURL;

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

    public void NewGame() {
        InitData();
        SceneManager.LoadScene(data.sceneName);
    }

    public void Save(GameData newData) {
        data = newData;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(getSaveFilePath());
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load() {
        if (File.Exists(getSaveFilePath())) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(getSaveFilePath(), FileMode.Open);
            data = (GameData)bf.Deserialize(file);
            file.Close();
            SceneManager.LoadScene(data.sceneName);
        }
    }

    public void Exit() {
        Application.Quit();
    }

    public void GameOver() {
        StartCoroutine(GameOverRoutine());
    }

    public void ChangeScene(GameData newData, string sceneName) {
        data = newData;
        if (sceneName == "Camp") {
            data.sceneName = "Camp";
            data.position[0] = 35f;
            data.position[1] = 0f;
            data.position[2] = 44f;
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

    private IEnumerator GameOverRoutine() {
        Debug.Log("Start game over");
        int score = data.totalNumberOfMeat * 1 + data.dayCount * 5 + data.survivorCount * 10;
        WWW www = new WWW(serverURL);
        yield return www;
        string result = www.text;
        string[] entities = result.Split('#');  // FORMAT name:score

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
