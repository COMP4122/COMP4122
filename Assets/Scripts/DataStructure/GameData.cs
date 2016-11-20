using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData {
    // The name of current scene
    public string sceneName;

    // Player's transform in the scene
    public float[] position;
    public float[] rotation;

    // Player's health
    public int health;

    // Player's weapon mode
    public ShootMode WeaponMode = ShootMode.Rock;

    // Number of days player has survived
    public int dayCount;

    // Number of meat player has
    public int meatCount;

    // Number of survivors in home
    public int survivorCount;

    // Total number of meat player has ever got
    public int totalNumberOfMeat;

    /* Status of quests
     * 0 - Not start
     * 100 - Finished
     * From 1 to 99 are customizable states
    */
    public int[] questList;
}
