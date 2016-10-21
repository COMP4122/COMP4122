using UnityEngine;

public class GetterUtility {

    public static Player GetPlayer() {
        return GameObject.Find("Player").GetComponent<Player>();
    }

    public static GameObject GetEnemyFlag()
    {
        return GameObject.Find("Enemy Flag");
    }

    public static GameObject GetFriendFlag() {
        return GameObject.Find("Friend Flag");
    }

    public static GameManager GetGameManager() {
        return GameObject.Find("GameManager").GetComponent<GameManager>();
    }

}

