using UnityEngine;
using System.Collections;

public class CloudsController : MonoBehaviour {
    public GameObject cloud1, cloud2, cloud3;
    private GameObject[] clouds;

	// Use this for initialization
	void Start () {
        clouds[0] = cloud1;
        clouds[1] = cloud2;
        clouds[2] = cloud3;
	}
	
	// Update is called once per frame
	void Update () {
        if (Random.Range(0.0f, 1.0f)>0.95) {
            Vector3 position = new Vector3(120.0f, 200.0f, Random.Range(-20.0f, 120.0f));
            Quaternion rotation = Quaternion.identity;
            Instantiate(clouds[Mathf.FloorToInt(Random.Range(0.0f, 3.0f))], position, rotation);
        }

	}
}
