using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event1 : MonoBehaviour {

    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    private DialogueManager theDM;
    private orderManager theOrder;
    private PlayerManager thePlayer; // diry=1
    private FadeManager theFade;

    private bool flag;

    // Use this for initialization
    void Start () {

        theDM = FindObjectOfType<DialogueManager>();
        theOrder = FindObjectOfType<orderManager>();
        thePlayer = FindObjectOfType<PlayerManager>();
        theFade = FindObjectOfType<FadeManager>();
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag && Input.GetKey(KeyCode.Z) && thePlayer.animator.GetFloat("Diry") == 1f)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }

    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacter();

        theOrder.NotMove();

        theDM.ShowDialogue(dialogue_1);

        yield return new WaitUntil(() => !theDM.talking);

        theOrder.Move("Player", "RIGHT");
        theOrder.Move("Player", "RIGHT");
        theOrder.Move("Player", "UP");

        yield return new WaitUntil(() => thePlayer.queue.Count==0);

        theFade.Flash();
        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => !theDM.talking);


        theOrder.Move();
    }
}
