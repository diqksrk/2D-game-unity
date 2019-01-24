using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour {

    private BoxCollider2D bound;
    public string boundName; //which bound for focusing.
    private CameraManager theCamera;

	// Use this for initialization
	void Start () {
        bound = GetComponent<BoxCollider2D>();
        theCamera = FindObjectOfType<CameraManager>();
	}

    public void SetBound()
    {
        if (theCamera != null)
        {
            theCamera.SetBound(bound);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
