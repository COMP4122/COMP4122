using UnityEngine;

public class RandomScale : MonoBehaviour {

    public float maxScale = 1.5f;
    public float minScale = 0.75f;
	// Use this for initialization
	void Start () {
		transform.localScale *= Random.Range(minScale, maxScale);
        //transform.Rotate(0, Random.Range(0, 180f), 0);
	}
}
