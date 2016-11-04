using UnityEngine;

public class GetterUtility {

    public static PrototypePlayer GetPlayer() {
        return GameObject.Find("Player").GetComponent<PrototypePlayer>();
    }

    public static PrototypeGameManager GetGameManager() {
        return GameObject.Find("GameManager").GetComponent<PrototypeGameManager>();
    }

}

