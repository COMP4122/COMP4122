using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardController : MonoBehaviour {
    private GameDataController dc;
    private TextMesh textField;

    void Start() {
        dc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameDataController>();
        textField = GetComponent<TextMesh>();
        StartCoroutine(getScoreFromServer());
    }

    IEnumerator getScoreFromServer() {
        WWW www = new WWW(dc.serverURL);
        yield return www;
        string result = www.text;
        string[] entities = result.Split('#');  // FORMAT name:score
        Entity[] entityList = new Entity[entities.Length - 1];
        Entity tmpEntity;
        for (int i = 0; i < entities.Length - 1; i++) {
            string[] tmp = entities[i].Split(':');
            tmpEntity = new Entity(tmp[0], int.Parse(tmp[1]));
            entityList[i] = tmpEntity;
        }
        for (int i = 0; i < entityList.Length - 1; i++)
            for (int j = i + 1; j < entityList.Length; j++) {
                if (entityList[j].score > entityList[i].score) {
                    Entity tmp = entityList[i];
                    entityList[i] = entityList[j];
                    entityList[j] = tmp;
                }
            }
        string text = "";
        for (int i = 0; i < 10; i++) {
            if (i >= entityList.Length) {
                break;
            }
            else {
                text += (i+1).ToString() + "\t\t" + entityList[i].name + "   " + entityList[i].score + "\n";
            }
        }
        textField.text = text;
    }
}

class Entity {
    public string name;
    public int score;

    public Entity(string name, int score) {
        this.name = name;
        this.score = score;
    }
}
