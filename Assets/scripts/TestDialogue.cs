using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour {

    [SerializeField]
    public Dialogue dialogue;

    private DialogueManager theDM;

	// Use this for initialization
	void Start () {
        theDM = FindObjectOfType<DialogueManager>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            theDM.ShowDialogue(dialogue);
        }
    }
}
