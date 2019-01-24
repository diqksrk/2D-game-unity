using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    private PlayerManager thePlayer;
    private Vector2 vector;
 

    private Quaternion rotation; // 회전을 담당하는 xy z w4개의 vector4

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = thePlayer.transform.position;

        vector.Set(thePlayer.animator.GetFloat("Dirx"), thePlayer.animator.GetFloat("Diry"));

        if (vector.x == 1f)
        {
            rotation = Quaternion.Euler(0, 0, 90);
            this.transform.rotation = rotation;
        }
        else if (vector.x == -1f) {
            rotation = Quaternion.Euler(0, 0, -90);
            this.transform.rotation = rotation;
        }
        else if (vector.y == 1f)
        {
            rotation = Quaternion.Euler(0, 0, 180);
            this.transform.rotation = rotation;
        }
        else if (vector.y == -1f)
        {
            rotation = Quaternion.Euler(0, 0, 0);
            this.transform.rotation = rotation;
        }
    }
}
