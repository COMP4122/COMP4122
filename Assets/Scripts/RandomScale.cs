using UnityEngine;

public class RandomScale : MonoBehaviour {

    private float maxScale = 1.5f;
    private float minScale = 0.75f;
	// Use this for initialization
	void Start () {
		transform.localScale *= Random.Range(minScale, maxScale);
        //transform.Rotate(0, Random.Range(0, 180f), 0);
	}
}
