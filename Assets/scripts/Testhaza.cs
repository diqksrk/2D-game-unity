using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testhaza : MonoBehaviour {

    [SerializeField]
    private orderManager theOrder;
    private DialogueManager theDM;

    public bool flag;
    public string[] texts;

	// Use this for initialization
	void Start () {
        theOrder = FindObjectOfType<orderManager>();
        //theNumber = FindObjectOfType<NumberSystem1>();
        theDM = FindObjectOfType<DialogueManager>();
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
        //theNumber.ShowNumber(correctNumber);
        //yield return new WaitUntil(()=>!theNumber.activated);
        theDM.ShowText(texts);
        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move();
    }
}
