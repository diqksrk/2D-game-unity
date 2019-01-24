using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestMove
{
    public string name;
    public string direction;
}

public class Test : MonoBehaviour {

    [SerializeField]
    //public TestMove[] move;
    public string direction;

    private orderManager theOrder;
	// Use this for initialization
	void Start () {
        theOrder = FindObjectOfType<orderManager>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            theOrder.PreLoadCharacter();
            //for (int i=0; i<move.Length; i++)
            //{
            //    theOrder.Move(move[i].name, move[i].direction);
            //}
            theOrder.Turn("npc1", direction);
        }
    }
}
