using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour {
    public AudioClip[] peaceBGM;
    public AudioClip[] fightBGM;

    private AudioSource audioSource;
    private int index = 0;

	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
        if (!audioSource.isPlaying) {
            audioSource.clip = peaceBGM[index];
            audioSource.Play();
            index++;
            index %= peaceBGM.Length;
        }
        
	}
}
