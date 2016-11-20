using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestStart : MonoBehaviour {

    public Camera playerplay;
    public Camera sceneplay;
    public GameObject deerAnimated;
    public GameObject deer;

    void Start () {
        deerAnimated.SetActive(true);
        deer.SetActive(false);
        playerplay.enabled = false;
        sceneplay.enabled = true;
        StartCoroutine(Switch());
    }

    IEnumerator Switch()
    {
        yield return new WaitForSeconds(3.0f);
        sceneplay.enabled = false;
        playerplay.enabled = true;
        yield return new WaitForSeconds(3.0f);
        deerAnimated.SetActive(false);
        deer.SetActive(true);
    }

}
