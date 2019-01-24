using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesabt : MonoBehaviour {

    public Choice choice;

    private orderManager theOrder;
    private ChoiceManager theChoice;

    public bool flag;

	// Use this for initialization
	void Start () {
        theOrder = FindObjectOfType<orderManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag)
        {
            StartCoroutine(ACoroutine());
        }
    }

    IEnumerator ACoroutine()
    {
        flag = true;
        theOrder.NotMove();
        theChoice.ShowChocie(choice);
        yield return new WaitUntil(()=>!theChoice.choiceIng);

        theOrder.Move();
        Debug.Log(theChoice.GetResult());
    }
}
