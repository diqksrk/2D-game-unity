using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public static Menu instance;

    public GameObject go;
    public AudioManager theAudio;

    public string call_sound;
    public string cancel_sound;

    public orderManager theOrder;

    public GameObject[] gos;

    private bool activated;
     
    public void Exit()
    {
        Application.Quit();
    }

    public void Continue()
    {
        activated = false;
        go.SetActive(false);
        theOrder.Move();
        theAudio.Play(cancel_sound);

    }

    public void GoToTitle()
    {
        for (int i=0; i<gos.Length; i++)
        {
            Destroy(gos[i]); 
        }
        go.SetActive(false);
        activated = false;
        SceneManager.LoadScene("title");
    }

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            activated = !activated;

            if (activated)
            {
                theOrder.NotMove();
                go.SetActive(true);
                theAudio.Play(call_sound);
            }
            else
            {
                go.SetActive(false);
                theAudio.Play(cancel_sound);
                theOrder.Move();
            }
        }
	}
}
