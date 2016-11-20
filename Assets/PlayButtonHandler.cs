using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonHandler : MonoBehaviour {

    private GameDataController dc;

    void Start() {
        dc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameDataController>();
    }

    void OnMouseDown() {
        if (name == "PlayBT")
            dc.NewGame();
        else if (name == "LoadBT")
            dc.Load();
    }
}
