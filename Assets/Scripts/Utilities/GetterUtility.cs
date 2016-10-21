using UnityEngine;

public class GetterUtility {

    public static Player GetPlayer() {
        return GameObject.Find("Player").GetComponent<Player>();
    }

    public static Flag GetEnemy()
    {
        return GameObject.Find("Flag_e").GetComponent<Flag>();
    }

    public static GameManager GetGameManager() {
        return GameObject.Find("GameManager").GetComponent<GameManager>();
    }

}

