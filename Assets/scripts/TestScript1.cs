using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript1 : MonoBehaviour {

    BGMManager BGM;

    public int playMusicTrack;

	// Use this for initialization
	void Start () {
        BGM = FindObjectOfType<BGMManager>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BGM.Play(playMusicTrack);
        this.gameObject.SetActive(false);
    }
}
