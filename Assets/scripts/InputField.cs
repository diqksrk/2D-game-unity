using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputField1 : MonoBehaviour {

    public Text text;
    private PlayerManager thePlayer;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerManager>();	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return))
        {
            thePlayer.characterName = text.text;
            Debug.Log(text.text);
            Destroy(this.gameObject);
        }
	}
}
